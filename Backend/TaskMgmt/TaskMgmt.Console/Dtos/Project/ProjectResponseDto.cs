namespace TaskMgmt.Console.Dtos
{
    public class ProjectResponseDto
    {
        public int projectId { get; set; }
        public int groupId { get; set; }
        public string projectName { get; set; } = null!;
        public string projectDescription { get; set; } = null!;
        public int ownerId { get; set; }
    }
}
