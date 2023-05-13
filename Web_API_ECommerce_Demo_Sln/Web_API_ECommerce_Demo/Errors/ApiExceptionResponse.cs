namespace Web_API_ECommerce_Demo.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null):base (statusCode, message)
        {
            Details = details;
        }
    }
}
