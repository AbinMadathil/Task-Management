namespace TaskMgmt.Services.CustomExceptions
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(string message) : base(message)
        {
        }
    }
}
