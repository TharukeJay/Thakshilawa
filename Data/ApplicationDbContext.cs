using XYZLaundry.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thakshilawa.Models;

namespace XYZLaundry.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Customer> Customers { get; set; }

       // public DbSet<Order> Orders { get; set; }

        public DbSet<ActivityLog> ActivityLog { get; set; }

       // public DbSet<Invoice> Invoices { get; set; }

       // public DbSet<Item> Items { get; set; }

       // public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Student> Student { get; set; }
<<<<<<< HEAD
        public DbSet<Classes> Classes { get; set; }
=======

        public DbSet<Attendence> Attendences { get; set; }

        public DbSet<Cafeteria> Cafeteria { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<ClassPayment> ClassPayment { get; set; }

        public DbSet<ClassSession> ClassSession { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<Payments> Payments { get; set; }
>>>>>>> 3c7b5a3faf21dc0411d334988b338e5aad368e13
    }
}
