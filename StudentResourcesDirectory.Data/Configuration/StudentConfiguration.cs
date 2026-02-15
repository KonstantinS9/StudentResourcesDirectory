using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentResourcesDirectory.Data.Models;

namespace StudentResourcesDirectory.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private readonly Student[] Students =
        {
            new Student { Id = 1, FirstName = "Ivan", LastName = "Petrov", FacultyNumber = "CS2023001", Email = "ivan.petrov@uni.bg", RegisteredOn = new DateTime(2026, 1, 19) },
            new Student { Id = 2, FirstName = "Maria", LastName = "Georgieva", FacultyNumber = "CS2022002", Email = "maria.georgieva@uni.bg", RegisteredOn = new DateTime(2025, 9, 1) },
            new Student { Id = 3, FirstName = "Georgi", LastName = "Dimitrov", FacultyNumber = "CS2021003", Email = "georgi.dimitrov@uni.bg", RegisteredOn = new DateTime(2025, 9, 1) },
            new Student { Id = 4, FirstName = "Elena", LastName = "Ivanova", FacultyNumber = "CS2019004", Email = "elena.ivanova@uni.bg", RegisteredOn = new DateTime(2025, 10, 11) },
            new Student { Id = 5, FirstName = "Petar", LastName = "Stoyanov", FacultyNumber = "CS2020005", Email = "petar.stoyanov@uni.bg", RegisteredOn = new DateTime(2026, 1, 21) },
            new Student { Id = 6, FirstName = "Viktoria", LastName = "Koleva", FacultyNumber = "CS2023006", Email = "viktoria.koleva@uni.bg", RegisteredOn = new DateTime(2024, 3, 8) },
            new Student { Id = 7, FirstName = "Dimitar", LastName = "Nikolov", FacultyNumber = "CS2022007", Email = "dimitar.nikolov@uni.bg", RegisteredOn = new DateTime(2024, 12, 19) },
            new Student { Id = 8, FirstName = "Kristina", LastName = "Petrova", FacultyNumber = "CS2021008", Email = "kristina.petrova@uni.bg", RegisteredOn = new DateTime(2024, 9, 1) },
            new Student { Id = 9, FirstName = "Alexander", LastName = "Ivanov", FacultyNumber = "CS2020009", Email = "alexander.ivanov@uni.bg", RegisteredOn = new DateTime(2025, 2, 16) },
            new Student { Id = 10, FirstName = "Simona", LastName = "Georgieva", FacultyNumber = "CS20190010", Email = "simona.georgieva@uni.bg", RegisteredOn = new DateTime(2025, 9, 1) }
        };


        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity
                .HasData(Students);
        }
    }
}