using System;
using System.Collections.Generic;

namespace TaskMgmt.Console.Dtos.Group
{
    public class GroupDTO
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public DateTime createdAt { get; set; }


        //public ICollection<Invitation> Invitations { get; set; } = null!;
        //public ICollection<Project> Projects { get; set; } = null!;
        //public ICollection<UserGroup> UserGroups { get; set; } = null!;
    }
}