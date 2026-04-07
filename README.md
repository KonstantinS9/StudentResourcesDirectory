# Student Resources Directory

## 📌 Overview

**Student Resources Directory** is an ASP.NET MVC web application designed to manage and organize educational resources for students. The platform allows users to browse, rate, comment, and manage different types of resources in a structured and user-friendly way.

---

## 🚀 Features

* 📚 Resource management (create, edit, delete)
* ⭐ Rating system for resources
* 💬 Comment system
* ❤️ Favorite resources
* 👤 Student management
* 🔍 Filtering resources by criteria
* 📄 Pagination for efficient data browsing
* 🔐 Authentication & Authorization (ASP.NET Identity)
* 🛠 Admin area for content management
* ✅ Unit testing for services

---

## 👥 User Roles

The application supports role-based access control:

* **Admin**

  * Full access to the system
  * Manage resources, users
  * Access to Admin Area

* **User**

  * Create and browse resources
  * Add comments and ratings
  * Add resources to favorites

---

## 🔐 Security

The application follows best practices for web security:

* ASP.NET Core Identity with User and Admin roles
* Role-based authorization via attributes and area-based access control
* Antiforgery tokens applied globally
* Client-side and server-side validation
* No raw SQL — EF Core LINQ queries only
* Custom error pages for **404 Not Found**, **500 Server Error** and **400 Bad Request**

---

## 🏗 Architecture

The project follows a layered architecture with clear separation of concerns:

### 🔹 Data Layer

* `StudentResourcesDirectory.Data`
* `StudentResourcesDirectory.Data.Models`

Handles database access and entity models.

---

### 🔹 Services Layer

* `StudentResourcesDirectory.Services.Core`

Contains business logic and core application services.

---

### 🔹 Web Layer (MVC)

* `StudentResourcesDirectory`

  * Controllers
  * Views
  * Areas (Admin)
  * Identity
  * wwwroot

Handles UI, routing, and user interaction.

---

### 🔹 ViewModels

* `StudentResourcesDirectory.ViewModels`

  * AdminViewModels
  * CommentViewModels
  * RatingViewModels
  * ResourceViewModels
  * StudentViewModels

Used for data transfer between controllers and views.

---

### 🔹 Common

* `StudentResourcesDirectory.GCommon`

  * ApplicationConstants
  * EntityValidation
  * ExceptionMessages

Shared utilities and constants.

---

### 🔹 Tests

* `StudentResourcesDirectory.Services.Tests`

Includes unit tests for:

* CommentService
* FavoriteService
* RatingService
* ResourceService
* StudentService
* ResourceManagementService

---

## ⚙️ Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* ASP.NET Identity
* NUnit

---

## ▶️ Getting Started

### Prerequisites

* .NET SDK (6 or later recommended)
* Visual Studio / VS Code
* SQL Server (or configured database)

---

### Installation

```bash id="c8yw36"
git clone https://github.com/your-username/StudentResourcesDirectory.git
cd StudentResourcesDirectory
```

```bash id="gn9i0k"
dotnet restore
dotnet build
dotnet run
```

---

## 🔐 Default Admin Account

The application comes with a preconfigured administrator account:

* **Email:** [konstantin@admin.com](mailto:konstantin@admin.com)
* **Password:** AdminRole1234


---

## 🔐 Admin Area

The application includes an **Admin panel** located at:

```id="qgm4h6"
/Admin
```

Used for managing resources and users.

---

## 🧪 Running Tests

```bash id="s0yqhp"
dotnet test
```

---

## 📁 Project Structure (Simplified)

```id="fwgx6l"
Data/
Services/
Tests/
Web/
ViewModels/
GCommon/
```

---

## 📌 Future Improvements

* REST API integration
* Performance optimizations
* Advanced search (full-text search)
* Cloud deployment (Azure/AWS)

---


---

## 📄 License

This project is for educational purposes.
