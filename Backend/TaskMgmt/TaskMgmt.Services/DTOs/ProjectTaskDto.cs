namespace TaskMgmt.Services.DTOs
{
    public class ProjectTaskDto
    {
        public int TaskId { get; set; }
        public string Description { get; set; } = null!;
        public string DueDate { get; set; } = null!;
        public string Assignee { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string CurrentStatus { get; set; } = null!;
    }
}
