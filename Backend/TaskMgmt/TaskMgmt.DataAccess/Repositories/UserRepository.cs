using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        //context
        private readonly TaskMgmntContext _taskMgmntContext;
        public UserRepository(TaskMgmntContext taskMgmntContext)
        {
            _taskMgmntContext = taskMgmntContext;
        }

        public  IEnumerable<User> Get()
        {
            var users =  _taskMgmntContext.Users.ToList();
            return users;
        }
        public  void Add(User user)
        {
            _taskMgmntContext.Users.Add(user);
        }
        public User? GetByEmail(string email)
        {
            var user =  _taskMgmntContext.Users.FirstOrDefault(user => user.Email == email);
            return user;
        }

        public User? GetByEmailAndGroup(string email,int grpId)
        {
           var user = _taskMgmntContext.Users
                                            .Include(e => e.UserGroups)
                                            .FirstOrDefault(e => e.Email == email && e.UserGroups.Any(p => p.GroupId == grpId));

            return user;
        }

        public  bool UserExists(string email)
        {
            return  _taskMgmntContext.Users.Any(u => u.Email == email);
        }

        public void EnrollUserToGroup(User user, Group group, bool isAdmin)
        {
            _taskMgmntContext.UserGroups.Add(new UserGroup
            {
                Group = group,
                User = user,
                IsAdmin = isAdmin,
            });
        }

        public bool IsMember(int userId, int groupId)
        {
            return  _taskMgmntContext.UserGroups.Any(x => x.GroupId == groupId && x.UserId == userId);

        }

        public  User GetById(int userId)
        {
            var user =  _taskMgmntContext.Users.FirstOrDefault(user => user.UserId == userId);
            return user;
        }
    }
}
