using YC3.Models;

namespace YC3.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicketAsync(string name, decimal price, int quantity);
        Task<string> PlaceOrderAsync(List<CartItemDto> items);
        Task<object> GetStatisticsAsync();
    }
}