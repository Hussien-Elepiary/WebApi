namespace Web_API_ECommerce_Demo.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "BadRequest",
                401 => "UnAuthorized",
                404 => "Resource Was Not Found",
                500 => "There was an Issue",
                _ => null
            };
        }
    }
}
