using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.Attributes;
using TaskMgmt.Api.DTO.Project;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.DataAccess.UnitOfWork;

namespace TaskMgmt.Api.Controllers
{
    [ApiController]
    [Route("api/groups/{groupId}/projects")]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTaskStatusRepository _projectTaskStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper, IProjectTaskStatusRepository projectTaskStatusRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _projectTaskStatusRepository = projectTaskStatusRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/groups/{groupId}/projects
        [HttpGet]
        [Authorize]
        [GroupMembershipAuthorize("groupId")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(int groupId)
        {
            try
            {
                var projects = _projectRepository.GetAll(groupId);
                await _unitOfWork.CommitAsync();
                var projectsResponse = _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);

                return Ok(projectsResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/groups/{groupId}/projects/{id}
        [HttpGet("{id}")]
        [Authorize]
        [GroupMembershipAuthorize("groupId")]
        public async Task<ActionResult<Project>> GetProject(int groupId, int id)
        {
            try
            {
                var project = _projectRepository.GetById(groupId, id);
                if (project == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CommitAsync();
                var projectResponseDto = _mapper.Map<ProjectResponseDto>(project);
                return Ok(projectResponseDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: /groups/{groupId}/projects
        [HttpPost]
        [Authorize]
        [GroupMembershipAuthorize("groupId")]
        public async Task<IActionResult> Create(int groupId, [FromBody] ProjectDto projectDto)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirst("UserId")?.Value);
                // TODO: Might need to check if userId null or invalid

                var project = _mapper.Map<Project>(projectDto);

                // Set GroupId and OwnerId explicitly
                project.GroupId = groupId;
                project.OwnerId = userId;

                _projectRepository.Create(project);
                _projectTaskStatusRepository.InitProjectStatus(project);
                await _unitOfWork.CommitAsync();
                var responseDto = _mapper.Map<ProjectResponseDto>(project);

                return CreatedAtAction(nameof(GetProject), new { groupId, id = project.ProjectId }, responseDto);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
