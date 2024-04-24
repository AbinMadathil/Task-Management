using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Services.DTOs
{
    public class NewTaskDto
    {
        public string Description { get; set; } = null!;
        public DateTime DueDate { get; set; }
        [EmailAddress]
        public string AssigneeMail { get; set; } = null!;
        public int CurrentStatusId { get; set; }
    }
}
