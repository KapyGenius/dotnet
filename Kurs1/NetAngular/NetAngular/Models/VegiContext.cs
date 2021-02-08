using System;
using Microsoft.EntityFrameworkCore;

namespace NetAngular.Models
{
    public class VegiContext:DbContext
    {
        public VegiContext(DbContextOptions<VegiContext> options):base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
