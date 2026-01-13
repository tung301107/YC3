using Microsoft.EntityFrameworkCore;
using YC3.Data;
using YC3.Interfaces;
using YC3.Models;

namespace YC3.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTicketAsync(string name, decimal price, int quantity)
        {
            _context.Tickets.Add(new ConcertTicket { Name = name, Price = price, AvailableQuantity = quantity });
            await _context.SaveChangesAsync();
        }

        public async Task<string> PlaceOrderAsync(List<CartItemDto> items)
        {
            if (items == null || !items.Any()) return "Giỏ hàng trống!";

            var order = new Order { OrderDate = DateTime.Now };

            foreach (var item in items)
            {
                var ticket = await _context.Tickets.FindAsync(item.TicketId);
                if (ticket == null) return $"Không tìm thấy vé ID {item.TicketId}";
                if (ticket.AvailableQuantity < item.Quantity) return $"Vé {ticket.Name} không đủ số lượng!";

                ticket.AvailableQuantity -= item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    TicketId = item.TicketId,
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return $"Đặt vé thành công! Mã đơn hàng (OrderId) của bạn là: {order.Id}";
        }

        public async Task<object> GetStatisticsAsync()
        {
            // .Include(oi => oi.Ticket) giúp EF truy cập được thông tin bảng Tickets khi đang đứng ở bảng OrderItems
            return await _context.OrderItems
                .Include(oi => oi.Ticket)
                .GroupBy(oi => oi.Ticket.Name)
                .Select(g => new {
                    TicketName = g.Key,
                    TotalSold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Quantity * x.Ticket.Price)
                })
                .ToListAsync();
        }
    }
}