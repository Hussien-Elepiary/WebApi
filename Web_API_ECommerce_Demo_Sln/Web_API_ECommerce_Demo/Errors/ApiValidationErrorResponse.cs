using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Web_API_ECommerce_Demo.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
