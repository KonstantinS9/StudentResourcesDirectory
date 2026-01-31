using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Models;

namespace StudentResourcesDirectory.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public virtual DbSet<Resource> Resources { get; set;  } = null!;
        public virtual DbSet<Category> Categories { get; set;  } = null!;
        public virtual DbSet<Student> Students { get; set;  } = null!;
    }
}
