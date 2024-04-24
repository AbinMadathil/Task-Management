using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.DataAccess.UnitOfWork;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskStatusService : IProjectTaskStatusService
    {
        private readonly IProjectTaskStatusRepository _projectTaskStatusRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskStatusService(
            IProjectTaskStatusRepository projectTaskStatusRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _projectTaskStatusRepository = projectTaskStatusRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ProjectTaskStatusDto> Add(int userId, int groupId, int projectId, ProjectTaskStatusCreateDto newStatus)
        {

            var user =  _userRepository.GetById(userId);
            var project = _projectRepository.GetById(groupId, projectId);
            if (project?.OwnerId != user.UserId) throw new Exception("User unauthorized");

            var status = new ProjectTaskStatus
            {
                ProjectId = projectId,
                StatusText = newStatus.StatusText,
                StatusColor = newStatus.StatusColor
            };
            _projectTaskStatusRepository.Add(status);

            await _unitOfWork.CommitAsync();

            return new ProjectTaskStatusDto
            {
                ProjectTaskStatusId = status.ProjectTaskStatusId,
                ProjectId = status.ProjectId,
                StatusText = status.StatusText,
                StatusColor = status.StatusColor,
            };
        }

        public async Task<IEnumerable<ProjectTaskStatusDto>> GetAll(int projectId)
        {
            var allStatuses = _projectTaskStatusRepository.GetAll();
            var projectStatus = allStatuses.ToList().Where(i => i.ProjectId == projectId);
            var results = new List<ProjectTaskStatusDto>();
            foreach (var status in projectStatus) {
                results.Add(new ProjectTaskStatusDto
                {
                    ProjectTaskStatusId = status.ProjectTaskStatusId,
                    ProjectId = status.ProjectId,
                    StatusText = status.StatusText,
                    StatusColor = status.StatusColor,
                });
            }

            return results;

        }

        public async Task<ProjectTaskStatusDto?> GetById(int id)
        {
            var task= _projectTaskStatusRepository.GetById(id);
            if (task == null) return null;               
            var obj = new ProjectTaskStatusDto{
                ProjectTaskStatusId = task.ProjectTaskStatusId,
                ProjectId = task.ProjectId,
                StatusText = task.StatusText,
                StatusColor= task.StatusColor
            };
            return obj;
        }
    }
}
