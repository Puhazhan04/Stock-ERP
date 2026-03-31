using Microsoft.EntityFrameworkCore;
using StockERP.Models;

namespace StockERP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;
    }
}
