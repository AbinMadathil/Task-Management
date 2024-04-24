using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.DataAccess.Models;

namespace TaskMgmt.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TaskMgmntContext _context;
        public GroupRepository(TaskMgmntContext context)
        {
            _context = context;
        }
        public void Add(Group group)
        {
            _context.Groups.Add(group);
        }
        public  Group GetById(int id)
        {
            return  _context.Groups.Find(id);
        }
        public Group[] GetAll(int userid)
        {
                        return _context.UserGroups
                              .Include(ug => ug.Group)
                              .Where(ug => ug.UserId == userid)
                              .Select(ug=>ug.Group)
                              .ToArray();
        }

        public bool CheckExists(string name)
        {
            return _context.Groups.Any(g => g.GroupName == name);
        }
        public int GenerateRefCode()
        {
            var random = new Random();
            return random.Next(100000, 999999);
        }
        public Invitation InviteUser(User user, Group group , string inviteeEmail)
        {
            var refcode = GenerateRefCode();
            var invitation = new Invitation
            {
                Group = group,
                InvitedByUserNavigation = user,
                InviteeEmail = inviteeEmail,
                Token = refcode.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            _context.Invitations.Add(invitation);
            return invitation;
        }
        public  Invitation GetInvitationByRefCode(string refCode)
        {
            return _context.Invitations.Include(x => x.Group).Where(x => x.Token == refCode).FirstOrDefault();
        }

        public void Enroll(UserGroup usergroup)
        {
            _context.UserGroups.Add(usergroup);
        }
        public void UpdateInvitation(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
        }

        //public async Task<int> GetGroupIdFromReferralCode(string referralCode)
        //{

        //    var group = await _context.Invitations
        //        .Where(rc => rc.Token == referralCode)
        //        .Select(rc => rc.GroupId)
        //        .FirstOrDefaultAsync();

        //    return (int)group;
        //}
    }
}