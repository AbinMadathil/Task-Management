namespace TaskMgmt.Services.CustomExceptions;
[Serializable]
public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string? message) : base(message)
    {
    }
}