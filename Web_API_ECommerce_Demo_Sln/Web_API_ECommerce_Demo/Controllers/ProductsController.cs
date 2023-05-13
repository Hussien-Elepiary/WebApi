using AutoMapper;
using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_ECommerce_Demo.Dtos;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,IMapper mapper)
        {
			_productsRepo = productsRepo;
            _mapper = mapper;
        }

        #region With out spec design pattern
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        //{
        //	var products = await _productsRepo.GetAllAsync();
        //	return Ok(products);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //	var product = await _productsRepo.GetByIdAsync(id);
        //	return Ok(product);
        //} 
        #endregion

        #region With spec design pattern
        [HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProductsWithSpec()
		{
			var spec = new ProductWithBrandAndTypeSpec();

			var products = await _productsRepo.GetAllWithSpecAsync(spec);

			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProductWithSpec(int id)
		{
			var spec = new ProductWithBrandAndTypeSpec(id);

			var product = await _productsRepo.GetWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));

			return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
		} 
		#endregion
	}
}
