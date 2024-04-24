using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectTaskStatusRepository
    {
        public IEnumerable<ProjectTaskStatus> GetAll();
        public ProjectTaskStatus? GetById(int id);
        public void Add(ProjectTaskStatus status);
        public void InitProjectStatus(Project project, Dictionary<string, string>? statusColorPairs = null);
    }
}
