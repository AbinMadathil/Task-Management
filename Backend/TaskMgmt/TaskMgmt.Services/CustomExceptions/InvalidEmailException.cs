namespace TaskMgmt.Services.CustomExceptions
{
    public class InvalidEmailException : Exception
    {

        public InvalidEmailException(string message)
            : base(message)
        {
        }

    }
}
