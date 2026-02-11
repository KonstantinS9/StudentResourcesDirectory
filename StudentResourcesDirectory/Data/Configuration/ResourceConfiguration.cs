using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentResourcesDirectory.Models;
using StudentResourcesDirectory.Models.Enums;

namespace StudentResourcesDirectory.Data.Configuration
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        private readonly Resource[] Resources =
        {
            new Resource
            {
                Id = 1,
                Title = "C# Fundamentals Book",
                Description = "Comprehensive guide to learn C# programming from scratch.",
                Url = "https://example.com/csharp-fundamentals",
                ResourceType = ResourceType.Book,
                CreatedOn = new DateTime(2026, 1, 1, 10, 0, 0),
                CategoryId = 1,  // Programming
                StudentId = 1
            },
            new Resource
            {
                Id = 2,
                Title = "Entity Framework Core Guide",
                Description = "Official guide to learn EF Core with examples.",
                Url = "https://example.com/ef-core-guide",
                ResourceType = ResourceType.Documentation,
                CreatedOn = new DateTime(2026, 1, 2, 11, 0, 0),
                CategoryId = 1,
                StudentId = 2
            },
            new Resource
            {
                Id = 3,
                Title = "ASP.NET MVC Video Series",
                Description = "Step-by-step video tutorials for MVC in ASP.NET.",
                Url = "https://youtube.com/aspnet-mvc",
                ResourceType = ResourceType.Video,
                CreatedOn = new DateTime(2026, 1, 3, 12, 0, 0),
                CategoryId = 1,
                StudentId = 3
            },
            new Resource
            {
                Id = 4,
                Title = "SQL Basics Article",
                Description = "Beginner-friendly introduction to SQL queries and concepts.",
                Url = "https://example.com/sql-basics",
                ResourceType = ResourceType.Article,
                CreatedOn = new DateTime(2026, 1, 4, 13, 0, 0),
                CategoryId = 2, // Databases
                StudentId = 4
            },
            new Resource
            {
                Id = 5,
                Title = "Linear Algebra Notes",
                Description = "Lecture notes covering linear algebra essentials.",
                Url = "https://example.com/linear-algebra",
                ResourceType = ResourceType.Book,
                CreatedOn = new DateTime(2026, 1, 5, 14, 0, 0),
                CategoryId = 3, // Mathematics
                StudentId = 5
            },
            new Resource
            {
                Id = 6,
                Title = "JavaScript Crash Course",
                Description = "Quick video tutorials to learn JavaScript fundamentals.",
                Url = "https://youtube.com/js-crash-course",
                ResourceType = ResourceType.Video,
                CreatedOn = new DateTime(2026, 1, 6, 15, 0, 0),
                CategoryId = 1,
                StudentId = 6
            },
            new Resource
            {
                Id = 7,
                Title = "Blazor Documentation",
                Description = "Official documentation to build web apps with Blazor.",
                Url = "https://docs.microsoft.com/blazor",
                ResourceType = ResourceType.Documentation,
                CreatedOn = new DateTime(2026, 1, 7, 16, 0, 0),
                CategoryId = 1,
                StudentId = 7
            },
            new Resource
            {
                Id = 8,
                Title = "Git & GitHub Guide",
                Description = "Complete guide for version control using Git and GitHub.",
                Url = "https://example.com/git-guide",
                ResourceType = ResourceType.Article,
                CreatedOn = new DateTime(2026, 1, 8, 17, 0, 0),
                CategoryId = 4, // Tools
                StudentId = 8
            },
            new Resource
            {
                Id = 9,
                Title = "Python for Data Science",
                Description = "Book covering Python basics and data science libraries.",
                Url = "https://example.com/python-ds",
                ResourceType = ResourceType.Book,
                CreatedOn = new DateTime(2026, 1, 9, 18, 0, 0),
                CategoryId = 1,
                StudentId = 9
            },
            new Resource
            {
                Id = 10,
                Title = "Responsive Web Design Tutorial",
                Description = "Learn responsive web design with HTML, CSS, and Bootstrap.",
                Url = "https://example.com/responsive-web",
                ResourceType = ResourceType.OnlineCourse,
                CreatedOn = new DateTime(2026, 1, 10, 19, 0, 0),
                CategoryId = 5, // Web Development
                StudentId = 10
            }
        };

        public void Configure(EntityTypeBuilder<Resource> entity)
        {
            entity
                .HasData(Resources);
        }
    }
}
