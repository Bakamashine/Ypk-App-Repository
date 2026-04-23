namespace Application.Common.Exceptions;

public class AccessException : Exception
{
    public AccessException() : base("Access denied")
    {
    }
}