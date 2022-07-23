using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_WebApi.Models
{
    public class Entity:IdentityDbContext<ApplicationUser>
    {
        public Entity() { }
        public Entity(DbContextOptions options) : base(options) { }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //   // modelBuilder.Entity<OrderItem>()
        //   //.HasKey(o => new { o.product_id, o.order_id });



        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<OrderItem> orderitems { get; set; }
        public DbSet<Order> orders { get; set; }
        //public DbSet<Customer> customers { get; set; }


    }
}
