using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class ProjectTaskStatusRepository : IProjectTaskStatusRepository
    {
        private readonly TaskMgmntContext _dBcontext;
        public ProjectTaskStatusRepository(TaskMgmntContext dBcontext)
        {
            _dBcontext = dBcontext;
        }

        public IEnumerable<ProjectTaskStatus> GetAll()
        {
            return _dBcontext.ProjectTaskStatuses
                            .Include(e => e.Project)
                            .ToList();
        }

        public ProjectTaskStatus? GetById(int Id)
        {
            var status = _dBcontext.ProjectTaskStatuses.Include(e => e.Project).Where(t => t.ProjectTaskStatusId == Id).SingleOrDefault();
            return status;
        }

        public void Add(ProjectTaskStatus status)
        {
            _dBcontext.ProjectTaskStatuses.Add(status);
            _dBcontext.SaveChanges();
        }

        public void InitProjectStatus(Project project, Dictionary<string, string>? statusColorPairs = null)
        {
            var DefaultOptions = new Dictionary<string, string>
            {
                { "open", "#FF0000" },
                { "in progress", "#FFFF00" },
                { "completed", "#00FF00" }
            };

            if (statusColorPairs is null)
            {
                statusColorPairs = DefaultOptions;
            }

            foreach (var (status, color) in statusColorPairs)
            {
                _dBcontext.Add(new ProjectTaskStatus
                {
                    StatusText = status,
                    StatusColor = color,
                    Project = project,
                });
            }
        }
    }
}
