namespace YC3.Models
{
    public class Order
    {
        public int Id { get; set; } // Đây chính là OrderId
        public DateTime OrderDate { get; set; }

        // Liên kết đến danh sách các vé được đặt trong đơn này
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}