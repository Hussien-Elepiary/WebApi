using ECommerce_Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(100);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var ProductToReturn = product.ToString();

            return Ok(ProductToReturn);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")] //GET : api/buggy/badrequest/five

        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }


    }
}
