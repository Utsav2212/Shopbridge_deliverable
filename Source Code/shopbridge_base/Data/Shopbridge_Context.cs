﻿using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;

namespace Shopbridge_base.Data
{
    public class Shopbridge_Context : DbContext
    {
        public Shopbridge_Context()
        {
        }
        public Shopbridge_Context(DbContextOptions<Shopbridge_Context> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}
