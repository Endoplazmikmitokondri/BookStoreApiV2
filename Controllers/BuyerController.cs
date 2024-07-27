using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "BuyerOnly")]
    public class BuyerController : ControllerBase
    {
        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            return Ok(new { message = "Buyer order history" });
        }
    }
}
