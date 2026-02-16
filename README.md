# 🚀 StudentResourcesDirectory

> A web application for managing and sharing student resources. Students can browse, add, and manage their own resources. Admins can manage users but cannot add resources.

.NET Version: 10.0  
ASP.NET Core: 10.0  
License: Apache-2.0

🔧 Requirements
.NET 10 
Visual Studio 2026
🚀 How to Run
Start the database
Make sure you have SQL Server installed locally (e.g., SQL Server Express or LocalDB). Start the SQL Server service using SQL Server Management Studio or Services.msc. No Docker is needed. Ensure the connection string in appsettings.json matches your local setup.

Run the app
Open the solution in Visual Studio and press F5, or from the terminal:

cd StudentResourcesDirectory
dotnet run

🔑 Admin Account
Email: konstantin@admin.com
Password: AdminRole1234
🛠️ Tech Stack
ASP.NET Core 10 MVC
Entity Framework Core 10.0.2 + SQL Server
ASP.NET Core Identity
Bootstrap 5.3
| Technology            | Version  | Purpose                          |
|-----------------------|----------|----------------------------------|
| ASP.NET Core MVC      | 10.0     | Web framework                    |
| Entity Framework Core | 10.0.2      | ORM / Database access            |
| SQL Server            | 21       | Database                         |
| Bootstrap             | 5.3      | Frontend styling                 |
| Razor Views           | -        | Server-side HTML rendering 

Structure:
StudentResourcesDirectory/
├── Controllers/        # MVC Controllers (ResourceController, StudentController, etc.)
├── Models/             # Domain models (Student, Resource, Category) and ViewModels
├── Views/              # Razor Views (.cshtml)
├── Services/           # Business logic / service layer
├── Data/               # ApplicationDbContext and EF Core migrations
├── wwwroot/            # Static files (CSS, JS, images)
├── Areas/Identity/     # Scaffolded Identity pages (Register, Login)
├── appsettings.json    # App configuration
└── Program.cs          # App entry point and middleware setup
