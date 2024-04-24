using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TaskMgmt.Api.Attributes;
using TaskMgmt.Api.DTO;
using TaskMgmt.Api.DTO.User;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Logger;
using Group = TaskMgmt.DataAccess.Models.Group;

namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userservice;
        private readonly ILoggerManager _logger;
        public GroupsController(IGroupService groupService,IUserService userservice ,ILoggerManager logger)
        {
            _groupService = groupService;
            _userservice = userservice;
            _logger = logger;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (int.TryParse(User.FindFirst("UserId").Value, out int userid))
                {
                    var groups = await _groupService.GetAll(userid);
                    _logger.LogInfo($"Retrieved groups for User '{userid}'.");
                    return Ok(groups);
                }
                else
                {
                    return BadRequest("Invalid UserId");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting groups: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GroupDTO groupDto)
        {
            try
            {
                if (groupDto == null)
                    return BadRequest();

                var group = new Group { GroupName = groupDto.GroupName };
                var userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var user = await _userservice.GetUserById(userId);
                var createdGroup = await _groupService.Add(group, user);
                _logger.LogInfo($"Group '{group.GroupName}' created by User '{userId}'.");
                return Created("Created", group);

            }
            catch (GroupAlreadyExistsException ex)
            {
                _logger.LogError($"Group creation failed: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding a group: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [GroupMembershipAuthorize("groupId")]
        [HttpPost("{groupId}/invitations")]
        public async Task<IActionResult> InviteUser(int groupId, [FromBody] EmailDTO email)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var invitationId = await _groupService.InviteUser(userId, groupId, email.Email);
                _logger.LogInfo($"User '{userId}' invited {email.Email} to Group '{groupId}'.");
                return Ok(invitationId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("enrollments")]
        [Authorize]
        public async Task<IActionResult> Enroll([FromBody] InvitationDTO invitation)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("UserId"));
                await _groupService.Enroll(userId, invitation.GroupName, invitation.ReferralCode);
                _logger.LogInfo($"User '{userId}' enrolled in Group '{invitation.GroupName}'.");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
