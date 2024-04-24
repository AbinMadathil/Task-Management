using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;
using TaskMgmt.DataAccess.UnitOfWork;

namespace TaskMgmt.DataAccess.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskMgmntContext _dBcontext;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTaskRepository(TaskMgmntContext dBcontext ,IUnitOfWork unit)
        {
            _dBcontext = dBcontext;
        }

        public  ICollection<ProjectTask> GetAll()
        {
            return  _dBcontext.ProjectTasks
                            .Include(e => e.Project)
                            .Include(e => e.Assignee)
                            .Include(e => e.Creator)
                            .Include(e => e.CurrentStatus)
                            .ToList();
        }

        public ProjectTask? GetById(int Id)
        {
            var task = _dBcontext.ProjectTasks.Where(t => t.ProjectTaskId == Id)
                            .Include(e => e.Assignee)
                            .Include(e => e.Creator)
                            .Include(e => e.CurrentStatus)
                            .SingleOrDefault();
            return task;
        }

        public void Add(ProjectTask task)
        {
            _dBcontext.ProjectTasks.Add(task);

        }

        public void Delete(ProjectTask reqtask)
        {
            _dBcontext.ProjectTasks.Remove(reqtask);

            _dBcontext.SaveChanges();
        }
    }
}
