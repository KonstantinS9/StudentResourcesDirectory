using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data.Configuration;
using StudentResourcesDirectory.Data.Models;

namespace StudentResourcesDirectory.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Resource>()
                .Property(b => b.ResourceType);

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ResourceConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
        }
    }
}
