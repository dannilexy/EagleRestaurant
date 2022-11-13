using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eagle.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Eagle.Services.ProductAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.britannica.com%2Ftopic%2Fpastry&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAD",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.megachicken.com.ng%2Fproduct%2Fmega-pastries%2F&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAO",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.britannica.com%2Ftopic%2Fpastry&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAD",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3DBUVY3MUJ1ws&psig=AOvVaw1E5BZS6FnObHOnobWL18t0&ust=1668454339755000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCID9-Pvyq_sCFQAAAAAdAAAAABAJ",
                CategoryName = "Entree"
            });
        }
    }
}
