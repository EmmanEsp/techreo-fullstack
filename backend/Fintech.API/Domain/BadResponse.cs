namespace Fintech.API.Domain;

public class BadResponse(string status, string message)
{
    public string Status { get; set; } = status;
    public string Message { get; set; } = message;

    public static BadResponse Fail(string message)
    {
        return new BadResponse("fail", message);
    }

    public static BadResponse Error(string message)
    {
        return new BadResponse("error", message);
    }
}
