using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.Attributes;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.ProjectTasks;


namespace TaskMgmt.Api.Controllers
{
    [Route("api/groups/{groupId}/projects/{projectId}/statuses")]
    [ApiController]
    public class ProjectTaskStatusController : ControllerBase
    {

        private readonly IProjectTaskStatusService _projectTaskStatusService;

        public ProjectTaskStatusController(IProjectTaskStatusService projectTaskStatusService)
        {
            this._projectTaskStatusService = projectTaskStatusService;
        }

        // GET: api/<TaskStatusesController>
        [HttpGet]
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
        public async Task<IActionResult> GetAll(int projectId)
        {
            var statuses = await _projectTaskStatusService.GetAll(projectId);
            if (!statuses.Any())
            {
                return NotFound();
            }
            return Ok(statuses);
        }

        // GET api/<TaskStatusesController>/5
        [HttpGet("{statusId}")]
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
        public async Task<IActionResult> GetById(int statusId)
        {
            var status = await _projectTaskStatusService.GetById(statusId);
            if(status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        // POST api/<TaskStatusesController>
        [HttpPost]
        [GroupMembershipAuthorize("groupId")]
        [Authorize]
        public async Task<IActionResult> Post([FromRoute] int groupId, [FromRoute] int projectId, [FromBody] ProjectTaskStatusCreateDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);

                var newStatus = await _projectTaskStatusService.Add(userId, groupId, projectId, value);
                return CreatedAtAction(nameof(GetById), new { projectId, groupId, statusId = newStatus.ProjectTaskStatusId }, newStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
