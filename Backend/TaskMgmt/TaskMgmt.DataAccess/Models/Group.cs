using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskMgmt.DataAccess.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<Invitation> Invitations { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; } = null!;
        [JsonIgnore]
        public ICollection<UserGroup> UserGroups { get; set; } = null!;
    }
}
