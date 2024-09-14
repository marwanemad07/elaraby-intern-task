namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetActiveCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(userId == null)
            {
                var badRequest = new RestDto<ShoppingCartDto?>(StatusCodes.Status400BadRequest);
                return BadRequest(badRequest);
            }

            var response = await _shoppingCartService.GetActiveCartAsync(userId);
            return Ok(response);
        }

        [HttpPost("cartitem")]
        [Authorize]
        public async Task<IActionResult> AddCartItem(NewCartItemDto newCartItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                var badRequest = new RestDto<ShoppingCartDto?>(StatusCodes.Status400BadRequest);
                return BadRequest(badRequest);
            }

            var response = await _shoppingCartService.AddCartItemAsync(newCartItem, userId);
            return Ok(response);
        }

        [HttpDelete("cartitem/{cartItemId}")]
        [Authorize]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                var badRequest = new RestDto<ShoppingCartDto?>(StatusCodes.Status400BadRequest);
                return BadRequest(badRequest);
            }

            var response = await _shoppingCartService.RemoveCartItemAsync(cartItemId, userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
