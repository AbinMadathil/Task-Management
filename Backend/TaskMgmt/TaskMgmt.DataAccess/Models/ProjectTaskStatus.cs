using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.DataAccess.Models
{
    public class ProjectTaskStatus
    {
        public int ProjectTaskStatusId { get; set; }
        public int ProjectId { get; set; }
        public string StatusText { get; set; } = null!;
        public string StatusColor { get; set; } = null!;

        public Project Project { get; set; } = null!;
    }
}
