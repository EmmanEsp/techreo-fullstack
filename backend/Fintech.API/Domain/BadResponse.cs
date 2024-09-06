namespace Fintech.API.Domain;

public class BadResponse
{
    public string Status { get; set; }
    public string Message { get; set; }

    public BadResponse(string status, string message)
    {
        Status = status;
        Message = message;
    }

    public static BadResponse Error(string message)
    {
        return new BadResponse("error", message);
    }

    public static BadResponse Fail(string message)
    {
        return new BadResponse("fail", message);
    }
}
