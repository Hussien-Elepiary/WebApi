using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]

    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
