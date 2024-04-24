namespace TaskMgmt.Api.DTO.Project
{
    public class ProjectResponseDto
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectDescription { get; set; } = null!;
        public int OwnerId { get; set; }
    }
}
