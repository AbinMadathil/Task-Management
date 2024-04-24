namespace TaskMgmt.Services.CustomExceptions;
[Serializable]
public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException(string? message) : base(message)
    {
    }
}