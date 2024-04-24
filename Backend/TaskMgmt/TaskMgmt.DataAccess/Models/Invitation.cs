using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public class Invitation
    {
        public int InvitationId { get; set; }
        public int? GroupId { get; set; }
        public int InvitedByUser { get; set; }
        public string InviteeEmail { get; set; } = null!;
        public string Token { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Group Group { get; set; } = null!;
        public User InvitedByUserNavigation { get; set; } = null!;
    }
}
