namespace Fintech.API.Domain;

public class Response<T>
{
    public string Status { get; set; }
    public T Data { get; set; }

    public Response(string status, T data)
    {
        Status = status;
        Data = data;
    }

    // Static methods to create common response types
    public static Response<T> Success(T data)
    {
        return new Response<T>("success", data);
    }

    public static Response<T> Fail(T data)
    {
        return new Response<T>("fail", data);
    }

    public static Response<T> Error(T data)
    {
        return new Response<T>("error", data);
    }
}
