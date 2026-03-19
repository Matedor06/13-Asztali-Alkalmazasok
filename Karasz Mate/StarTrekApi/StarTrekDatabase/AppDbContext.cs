using Microsoft.EntityFrameworkCore;
using StarTrekDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarTrekDatabase
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ShipEntity> Ships { get; set; }
    }
}
