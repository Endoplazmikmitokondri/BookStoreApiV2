using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "SellerOnly")]
    public class SellerController : ControllerBase
    {
        [HttpGet("sales")]
        public IActionResult GetSales()
        {
            return Ok(new { message = "Seller sales data" });
        }
    }
}
