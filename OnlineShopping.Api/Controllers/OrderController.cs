using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        // TODO: this must be valid for admins only
        [Authorize]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("myorders")]
        [Authorize]
        public async Task<IActionResult> GetCustomerOrder()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(customerId == null)
            {
                var badRequestResult = new RestDto<OrderDto?>(StatusCodes.Status400BadRequest);
                return BadRequest(badRequestResult);
            }

            var result = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(customerId == null)
            {
                var badRequestResult = new RestDto<OrderDto?>(StatusCodes.Status400BadRequest);
                return BadRequest(badRequestResult);
            }

            var result = await _orderService.CreateOrderAsync(customerId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
