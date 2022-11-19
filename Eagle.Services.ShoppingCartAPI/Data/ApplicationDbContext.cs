using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eagle.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Eagle.Services.ShoppingCartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
