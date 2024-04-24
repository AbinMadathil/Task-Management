using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskMgmt.DataAccess.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool? IsAdmin { get; set; } = false;

        [JsonIgnore]
        public Group Group { get; set; } = null!;
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
