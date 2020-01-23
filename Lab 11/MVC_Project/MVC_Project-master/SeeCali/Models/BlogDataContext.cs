using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SeeCali.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public IQueryable<MonthlySpecial28> MonthlySpecials
        {
            get
            {
                return new[]
                {
                    new MonthlySpecial28 {
                        Key = "calm",
                        Name = "California Calm Package",
                        Type = "Day Spa Package",
                        Price = 250,
                    },
                    new MonthlySpecial28 {
                        Key = "desert",
                        Name = "From Desert to Sea",
                        Type = "2 Day Salton Sea",
                        Price = 350,
                    },
                    new MonthlySpecial28 {
                        Key = "backpack",
                        Name = "Backpack Cali",
                        Type = "Big Sur Retreat",
                        Price = 620,
                    },
                    new MonthlySpecial28 {
                        Key = "taste",
                        Name = "Taste of California",
                        Type = "Tapas & Groves",
                        Price = 150,
                    },
                }.AsQueryable();
            }
        }

        public BlogDataContext(DbContextOptions<BlogDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
