using OnlineShopping.Infrastructure.Enums;
using OnlineShopping.Infrastructure.Specifications;

namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? sort, string? search, int? pageSize, int? pageNumber)
        {
            var parameters = new PageSpecificationParameters(sort, search, pageSize, pageNumber);
            var result = await _productService.GetAllAsync(parameters);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task<IActionResult> Add([FromForm] NewProductDto productDto)
        {
            var result = await _productService.AddAsync(productDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
