using Microsoft.AspNetCore.Mvc;
using YC3.Interfaces;
using YC3.Models;

namespace YC3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        // DI Container sẽ tiêm ITicketService đã đăng ký ở Program.cs vào đây
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("create-ticket")]
        public async Task<IActionResult> Create(string name, decimal price, int quantity)
        {
            await _ticketService.CreateTicketAsync(name, price, quantity);
            return Ok("Tạo loại vé mới thành công.");
        }

        // Thay đổi API này để đặt được nhiều vé cùng lúc (OrderId chung)
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] List<CartItemDto> items)
        {
            var result = await _ticketService.PlaceOrderAsync(items);
            if (result.Contains("Không tìm thấy") || result.Contains("không đủ"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            return Ok(await _ticketService.GetStatisticsAsync());
        }
    }
}