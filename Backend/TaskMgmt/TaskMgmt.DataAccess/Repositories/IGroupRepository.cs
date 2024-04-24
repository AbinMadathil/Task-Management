using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IGroupRepository
    {
        public Group GetById(int id);
        public Group[] GetAll(int userid);
        public void Add(Group group);
        public bool CheckExists(string name);
        public Invitation InviteUser(User user ,Group group, string inviteeEmail);
        public Invitation GetInvitationByRefCode(string refCode);

        public void Enroll(UserGroup usergroup);
        public void UpdateInvitation(Invitation invitation);

       // public Task InviteUSer(Invitation invitation);
       // public Task<int> GetGroupIdFromReferralCode(string referralCode);
    }
}