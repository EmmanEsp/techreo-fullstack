namespace Fintech.API.Domain;

public class SuccessResponse<T>(string status, T data)
{
    public string Status { get; set; } = status;
    public T Data { get; set; } = data;

    public static SuccessResponse<T> Success(T data)
    {
        return new SuccessResponse<T>("success", data);
    }
}
