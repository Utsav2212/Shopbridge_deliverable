namespace Shopbridge_base.Domain.Models
{
    
    public class ApiResponse<T>
    {
        public ApiResponse(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
