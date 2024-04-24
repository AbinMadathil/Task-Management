using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.Services
{
    public interface IGroupService
    {
        Task<Group> GetById(int id);
        Task<Group[]> GetAll(int userid);
        Task<int> Add(Group group, User user);
        public Task<int> InviteUser(int userId, int groupId, string inviteeEmail);
        Task Enroll(int userId, string groupName, string ReferralCode);
    }
}