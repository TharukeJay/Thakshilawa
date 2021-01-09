﻿using XYZLaundry.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Order> Orders { get; set; }

        public DbSet<ActivityLog> ActivityLog { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
