namespace TaskMgmt.Services.CustomExceptions;

[Serializable]
public class AssigneeNotFoundException : Exception
{
    public AssigneeNotFoundException(string? message) : base(message)
    {
    }
}
