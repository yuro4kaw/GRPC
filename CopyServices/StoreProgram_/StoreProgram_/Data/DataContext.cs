﻿using Microsoft.EntityFrameworkCore;
using StoreProgram_.Model;

namespace StoreProgram_.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}
