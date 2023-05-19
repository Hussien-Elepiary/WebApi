using AutoMapper;
using ECommerce_Demo_Core.Entities.Products;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.Specifications;
using ECommerce_Demo_Core.Specifications.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API_ECommerce_Demo.Dtos;
using Web_API_ECommerce_Demo.Errors;
using Web_API_ECommerce_Demo.Helpers;

namespace Web_API_ECommerce_Demo.Controllers.Bussines
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
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
        //[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProductsWithSpec([FromQuery] ProductSpecParams productSPec)
        {
            var spec = new ProductWithBrandAndTypeSpec(productSPec);

            var products = await _productsRepo.GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductSpecForCount(productSPec);

            var count = await _productsRepo.GetCountWithSpec(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productSPec.PageIndex, productSPec.PageSize, count, data));
        }

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductWithSpec(int id)
        {
            var spec = new ProductWithBrandAndTypeSpec(id);

            var product = await _productsRepo.GetWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
        }
        #endregion
    }
}
