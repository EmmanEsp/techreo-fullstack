namespace Fintech.API.Domain;

public class ServiceException(string message, int statusCode = 400) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
