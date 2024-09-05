namespace Fintech.API.Domain;

public class ServiceResponse<T>(string status, T data)
{
    public string Status { get; set; } = status;
    public T Data { get; set; } = data;

    public static ServiceResponse<T> Success(T data)
    {
        return new ServiceResponse<T>("success", data);
    }
}
