using VideoCall.Core.Shared;

namespace VideoCall.Application.Abstractions.ApiResponse;

public class GenericResponse<T>
{
    public bool Success { get; set; }
    public Error? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public GenericResponse(T data)
    {
        Success = true;
        ErrorMessage = null;
        Data = data;
    }

    public GenericResponse(Error error)
    {
        Success = false;
        ErrorMessage = error;
        Data = default;
    }
}
