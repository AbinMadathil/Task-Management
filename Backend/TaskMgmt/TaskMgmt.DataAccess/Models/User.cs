using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskMgmt.DataAccess.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username {get; set;} = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? DefaultGroupId { get; set; }

        [JsonIgnore]
        public Group DefaultGroup { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Invitation> Invitations { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; } = null!;
        [JsonIgnore]
        public ICollection<UserGroup> UserGroups { get; set; } = null!;
    }
}
