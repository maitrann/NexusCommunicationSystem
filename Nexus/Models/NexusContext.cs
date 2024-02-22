using Microsoft.EntityFrameworkCore;

namespace Nexus.Models
{
    public class NexusContext : DbContext
    {
        public NexusContext() 
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-BBVNKGM;Initial Catalog=Nexus;Persist Security Info=True;TrustServerCertificate=True;" +
                "User ID=sa;Password=123");
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<ExpiryDate> ExpiryDates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ServicePlan> ServicePlans { get; set; }
        public DbSet<ProductExchange> ProductExchanges { get; set; }


    }
}
