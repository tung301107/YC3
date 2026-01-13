using System.Collections.Generic;
using YC3.Models;
using Microsoft.EntityFrameworkCore;

namespace YC3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ConcertTicket> Tickets { get; set; } // Tương ứng bảng Tickets
        public DbSet<Order> Orders { get; set; }         // Tương ứng bảng Orders
        public DbSet<OrderItem> OrderItems { get; set; } // Tương ứng bảng OrderItems
    }
}
