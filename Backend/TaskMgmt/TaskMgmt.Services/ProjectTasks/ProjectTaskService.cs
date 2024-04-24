using System.Threading.Tasks.Dataflow;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services.DTOs;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Interfaces;
using TaskMgmt.DataAccess.UnitOfWork;
using System.Threading.Tasks;

namespace TaskMgmt.Services.ProjectTasks
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskService(
            INotificationService notificationService,
            IProjectTaskRepository projectTaskRepo,
            IUserRepository userRepo,
            IProjectRepository projectRepo,
            IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _projectTaskRepository = projectTaskRepo;
            _userRepository = userRepo;
            _projectRepository = projectRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectTaskDto?> Get(int taskId)
        {
            var currentTask = _projectTaskRepository.GetById(taskId);
            if (currentTask == null)
            {
                return null;
            }

            var projectTaskDTo = new ProjectTaskDto
            {
                TaskId = currentTask.ProjectTaskId,
                Description = currentTask.Description,
                DueDate = currentTask.DueDate.ToShortTimeString(),
                Assignee = currentTask.Assignee.Email,
                CreatedBy = currentTask.Creator.Email,
                CurrentStatus = currentTask.CurrentStatus.StatusText
            };
            return projectTaskDTo;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAll(int groupId, int projectId)
        {
            var allProjectTasks = _projectTaskRepository.GetAll();
            var currProjectTasks = allProjectTasks
                        .Where(a => a.ProjectId == projectId && a.Project.GroupId == groupId)
                        .ToList();
            var results = new List<ProjectTaskDto>();

            foreach(var task in  currProjectTasks)
            {
                results.Add(new ProjectTaskDto
                {
                    TaskId = task.ProjectTaskId,
                    Description = task.Description,
                    DueDate = task.DueDate.ToString(),
                    Assignee = task.Assignee.Email,
                    CreatedBy = task.Creator.Email,
                    CurrentStatus = task.CurrentStatus.StatusText

                });
            }
            return results;
        }

        public async Task<ProjectTaskDto> CreateTask(int userId, int groupId, int projectId, NewTaskDto newTask)
        {
            var project = _projectRepository.GetById(groupId, projectId);
            if (project is null)
            {
                throw new ProjectNotFoundException("Project not found");
            }
            var assignee =_userRepository.GetByEmail(newTask.AssigneeMail);
            // var assignee = _userRepository.GetByEmailAndGroup(newTask.AssigneeMail,groupId);
            if (assignee is null)
            {
                throw new AssigneeNotFoundException("Assignee not found");
            }

            var task = new ProjectTask
            {
                Description = newTask.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = newTask.DueDate,
                CreatorId = userId,
                AssigneeId = assignee.UserId,
                ProjectId = projectId,
                CurrentStatusId = newTask.CurrentStatusId
            };
            _projectTaskRepository.Add(task);
            await _unitOfWork.CommitAsync();
            await _notificationService.NotifyAsync(task.Assignee.Email, "New Task Assigned", task.Description);
            var created = _projectTaskRepository.GetById(task.ProjectTaskId);

            return new ProjectTaskDto
            {
                TaskId = task.ProjectTaskId,
                Description = task.Description,
                DueDate = task.DueDate.ToShortDateString(),
                Assignee = task.Assignee.Email,
                CreatedBy = task.Creator.Email,
                CurrentStatus = created?.CurrentStatus?.StatusText ?? "Unable to retrieve status",
            };
        }

        public async Task DeleteTask(int taskid,int projectid,int groupid)
        {
            var FoundProject = _projectRepository.GetById(groupid, projectid);
            if (FoundProject == null)
            {
                throw new Exception("Project not Found"); 
            }
            var ReqTask=FoundProject.ProjectTasks.FirstOrDefault(p=>p.ProjectTaskId == taskid);
            _projectTaskRepository.Delete(ReqTask);
            //return new ProjectTaskDto
            //{
            //    TaskId = ReqTask.ProjectTaskId,
            //    Description = ReqTask.Description,
            //    DueDate = ReqTask.DueDate.ToShortDateString(),
            //    Assignee = ReqTask.Assignee.Email,
            //    CreatedBy = ReqTask.Creator.Email,
            //};
        }
    }
}
