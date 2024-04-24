using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public interface IProjectTaskService
    {
        public Task<ProjectTaskDto> CreateTask(int userId, int groupId, int projectId, NewTaskDto newTask);
        public Task<ProjectTaskDto?> Get(int taskId);
        public Task<IEnumerable<ProjectTaskDto>> GetAll(int groupId, int projectId);

        public Task DeleteTask(int taskid, int projectid, int groupid);
    }
}
