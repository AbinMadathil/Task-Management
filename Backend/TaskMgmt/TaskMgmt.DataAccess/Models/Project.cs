using System;
using System.Collections.Generic;

namespace TaskMgmt.DataAccess.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Group Group { get; set; } = null!;
        public User Owner { get; set; } = null!;
        public ICollection<ProjectTask> ProjectTasks { get; set; } = null!;
        public ICollection<ProjectTaskStatus> ProjectTaskStatuses { get; set; } = null!;
    }
}
