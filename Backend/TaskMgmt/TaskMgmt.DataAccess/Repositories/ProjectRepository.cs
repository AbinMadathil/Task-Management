using Microsoft.EntityFrameworkCore;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{

    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskMgmntContext _dbcontext;
        public ProjectRepository(TaskMgmntContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Project> GetAll(int groupId)
        {
            return _dbcontext.Projects.Where(p => p.GroupId == groupId).ToList();
        }

        public Project? GetById(int groupId, int id)
        {
            return _dbcontext.Projects.Include(e => e.ProjectTasks).FirstOrDefault(p => p.GroupId == groupId && p.ProjectId == id);
        }

        public void Create(Project project)
        {
            _dbcontext.Projects.Add(project);
        }

        public void Edit(Project project)
        {
            _dbcontext.Entry(project).State = EntityState.Modified;
        }

        public void Delete(int groupId, int id)
        {
            var project = _dbcontext.Projects.FirstOrDefault(p => p.GroupId == groupId && p.ProjectId == id);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }

            _dbcontext.Projects.Remove(project);
        }


        [Obsolete($"☠️☠️☠️ Use {nameof(GetById)} method instead ☠️☠️☠️", false)]
        public Task<Project?> GetByIdAsync(int groupId, int id)
        {
            return _dbcontext.Projects.FirstOrDefaultAsync(p => p.GroupId == groupId && p.ProjectId == id);
        }


    }
}
