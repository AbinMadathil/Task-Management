namespace TaskMgmt.DataAccess.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public int AssigneeId { get; set; }
        public int CreatorId { get; set; }
        public int ProjectId { get; set; }
        public int CurrentStatusId { get; set; }
        public bool isDraft {get; set;} = false;

        public User Assignee { get; set; } = null!;
        public User Creator { get; set; } = null!;
        public Project Project { get; set; } = null!;
        public ProjectTaskStatus CurrentStatus { get; set; } = null!;
    }
}
