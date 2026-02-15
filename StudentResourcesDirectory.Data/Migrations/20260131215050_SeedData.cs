using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentResourcesDirectory.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Programming" },
                    { 2, "Databases" },
                    { 3, "Mathematics" },
                    { 4, "Tools" },
                    { 5, "Web Development" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FacultyNumber", "FirstName", "LastName", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, "ivan.petrov@uni.bg", "CS2023001", "Ivan", "Petrov", new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "maria.georgieva@uni.bg", "CS2022002", "Maria", "Georgieva", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "georgi.dimitrov@uni.bg", "CS2021003", "Georgi", "Dimitrov", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "elena.ivanova@uni.bg", "CS2019004", "Elena", "Ivanova", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "petar.stoyanov@uni.bg", "CS2020005", "Petar", "Stoyanov", new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "viktoria.koleva@uni.bg", "CS2023006", "Viktoria", "Koleva", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "dimitar.nikolov@uni.bg", "CS2022007", "Dimitar", "Nikolov", new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "kristina.petrova@uni.bg", "CS2021008", "Kristina", "Petrova", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "alexander.ivanov@uni.bg", "CS2020009", "Alexander", "Ivanov", new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "simona.georgieva@uni.bg", "CS20190010", "Simona", "Georgieva", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "StudentId", "Title", "Type", "Url" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Comprehensive guide to learn C# programming from scratch.", 1, "C# Fundamentals Book", 0, "https://example.com/csharp-fundamentals" },
                    { 2, 1, new DateTime(2026, 1, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "Official guide to learn EF Core with examples.", 2, "Entity Framework Core Guide", 5, "https://example.com/ef-core-guide" },
                    { 3, 1, new DateTime(2026, 1, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Step-by-step video tutorials for MVC in ASP.NET.", 3, "ASP.NET MVC Video Series", 2, "https://youtube.com/aspnet-mvc" },
                    { 4, 2, new DateTime(2026, 1, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "Beginner-friendly introduction to SQL queries and concepts.", 4, "SQL Basics Article", 3, "https://example.com/sql-basics" },
                    { 5, 3, new DateTime(2026, 1, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), "Lecture notes covering linear algebra essentials.", 5, "Linear Algebra Notes", 0, "https://example.com/linear-algebra" },
                    { 6, 1, new DateTime(2026, 1, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), "Quick video tutorials to learn JavaScript fundamentals.", 6, "JavaScript Crash Course", 2, "https://youtube.com/js-crash-course" },
                    { 7, 1, new DateTime(2026, 1, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), "Official documentation to build web apps with Blazor.", 7, "Blazor Documentation", 5, "https://docs.microsoft.com/blazor" },
                    { 8, 4, new DateTime(2026, 1, 8, 17, 0, 0, 0, DateTimeKind.Unspecified), "Complete guide for version control using Git and GitHub.", 8, "Git & GitHub Guide", 3, "https://example.com/git-guide" },
                    { 9, 1, new DateTime(2026, 1, 9, 18, 0, 0, 0, DateTimeKind.Unspecified), "Book covering Python basics and data science libraries.", 9, "Python for Data Science", 0, "https://example.com/python-ds" },
                    { 10, 5, new DateTime(2026, 1, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), "Learn responsive web design with HTML, CSS, and Bootstrap.", 10, "Responsive Web Design Tutorial", 4, "https://example.com/responsive-web" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
