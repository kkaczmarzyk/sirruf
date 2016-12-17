using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sirruf.Models;

namespace Sirruf.Data
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

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);    


        }

        public DbSet<Purchase> Purchase { get; set; }

        public DbSet<Shop> Shop { get; set; }

        public DbSet<Brand> Brand { get; set; }

        //public DbSet<Comment> Comment { get; set; }

        public DbSet<Category> Category { get; set; }
    }
}
