namespace TaskMgmt.Services.CustomExceptions;
[Serializable]
public class GroupNotFoundException : Exception
{
    public GroupNotFoundException(string? message) : base(message)
    {
    }
}