namespace Domain.Entity
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(string message = "", int code = 0)
        {
            Message = message;
            StatusCode = code;
        }
    }
}