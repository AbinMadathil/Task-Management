using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.Services.DTOs;

namespace TaskMgmt.Services.ProjectTasks
{
    public interface IProjectTaskStatusService
    {

        public Task<IEnumerable<ProjectTaskStatusDto>> GetAll(int projectId);
        public Task<ProjectTaskStatusDto?> GetById(int taskId);
        public Task<ProjectTaskStatusDto> Add(int userId, int groupId, int projectId, ProjectTaskStatusCreateDto status);
    }
}
