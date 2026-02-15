using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentResourcesDirectory.Data.Models;

namespace StudentResourcesDirectory.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private readonly Category[] Categories =
        {
            new Category { Id = 1, Name = "Programming" },
            new Category { Id = 2, Name = "Databases" },
            new Category { Id = 3, Name = "Mathematics" },
            new Category { Id = 4, Name = "Tools" },
            new Category { Id = 5, Name = "Web Development" }
        };

        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity
                .HasData(Categories);
        }
    }
}