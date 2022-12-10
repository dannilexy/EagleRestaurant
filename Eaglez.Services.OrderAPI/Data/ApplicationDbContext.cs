﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eaglez.Services.OrderAPI.Models;

namespace Eaglez.Services.OrderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderHeader> OrderHeader { get; set; } = default!;
        public DbSet<OrderDetails>  OrderDetails { get; set; } = default!;
    }
}
