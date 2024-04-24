using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll(int groupId);
        Project? GetById(int groupId, int id);
        void Create(Project project);
        void Edit(Project project);
        void Delete(int groupId, int id);


        [Obsolete($"☠️☠️☠️ Use {nameof(GetById)} method instead ☠️☠️☠️", false)]
        Task<Project?> GetByIdAsync(int groupId, int id);
    }
}
