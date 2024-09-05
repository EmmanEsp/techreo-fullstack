namespace Fintech.API.Domain;

public class ValidationErrorResponse(string status, Dictionary<string, string> errors)
{
    public string Status { get; set; } = status;

    public Dictionary<string, string> Message { get; set; } = errors;

    public static ValidationErrorResponse Fail(Dictionary<string, string> errors)
    {
        return new ValidationErrorResponse("fail", errors);
    }
}
