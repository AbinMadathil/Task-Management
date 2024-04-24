using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectTaskRepository
    {
        public ICollection<ProjectTask> GetAll();
        public ProjectTask? GetById(int Id);
        public void Add(ProjectTask task);
        public void Delete(ProjectTask reqtask);
    }
}
