namespace YC3.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties để DI và EF Core có thể truy vấn thông tin
        public virtual Order? Order { get; set; }
        public virtual ConcertTicket? Ticket { get; set; }
    }
}