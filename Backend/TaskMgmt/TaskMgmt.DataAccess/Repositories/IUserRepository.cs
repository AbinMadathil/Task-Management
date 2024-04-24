using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> Get();
        public User GetByEmail(string email);
        public User GetById(int userId);
        public void Add(User user);
        public bool UserExists(string email);
        public void EnrollUserToGroup(User user,Group group, bool isAdmin);

        public User GetByEmailAndGroup(string email,int grpId);

        public bool IsMember(int userId,int groupId);
    }
}