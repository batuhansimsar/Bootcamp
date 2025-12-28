# 🚀 Bootcamp Management System

A modern, full-stack web application designed to manage technology bootcamps. This platform facilitates the interaction between Applicants, Instructors, and Employees, streamlining the process of bootcamp creation, application, and management.

## 🛠️ Technology Stack

This project is built using **Clean Architecture** principles to ensure scalability and maintainability.

### Backend (.NET 8 Web API)
-   **Core:** ASP.NET Core 8
-   **Architecture:** Clean Architecture (N-Layer)
-   **Data Access:** Entity Framework Core
-   **Database:** SQL Server / PostgreSQL (Configurable)
-   **Authentication:** JWT (JSON Web Tokens)
-   **Logging:** Serilog
-   **Mapping:** AutoMapper
-   **Validation:** FluentValidation

### Frontend (React)
-   **Framework:** React 18
-   **Build Tool:** Vite
-   **Language:** TypeScript
-   **Styling:** Modern CSS3 (Glassmorphism design), Responsive Layouts
-   **State Management:** Context API
-   **HTTP Client:** Axios

## ✨ Features

-   **Authentication & Authorization**: Secure login system with role-based access control (Applicant, Instructor, Employee).
-   **Bootcamp Management**: Create, update, delete, and list bootcamps.
-   **Application Process**: Applicants can view available bootcamps and apply.
-   **Rich Dashboard**:
    -   Visualize bootcamp status (Preparing, Open, Started, Completed).
    -   Responsive grid layout.
    -   Role-specific views (e.g., "Apply" button only visible to Applicants).
-   **Pagination**: Efficient data handling for large lists of bootcamps.

## 🚀 Getting Started

Follow these instructions to get the project running on your local machine.

### Prerequisites
-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Node.js](https://nodejs.org/) (Latest LTS)
-   SQL Server or compatible database

### 1. Backend Setup

 Navigate to the solution folder:
 \`\`\`bash
 cd Bootcamp
 \`\`\`

 Restore dependencies:
 \`\`\`bash
 dotnet restore
 \`\`\`

 Update the database (Ensure your connection string in `appsettings.json` is correct):
 \`\`\`bash
 dotnet ef database update --project Bootcamp.Repositories --startup-project Bootcamp.WebAPI
 \`\`\`

 Run the API:
 \`\`\`bash
 dotnet run --project Bootcamp.WebAPI
 \`\`\`
 *The backend will start on `http://localhost:5158`*

### 2. Frontend Setup

 Navigate to the frontend folder:
 \`\`\`bash
 cd frontend
 \`\`\`

 Install dependencies:
 \`\`\`bash
 npm install
 \`\`\`

 Start the development server:
 \`\`\`bash
 npm run dev
 \`\`\`
 *The frontend will start on `http://localhost:5173`*

## 🔮 Future Improvements

The following features are planned for future releases:

-   [ ] **Email Notifications**: Integration with SMTP to send emails for registration and application updates.
-   [ ] **Advanced Reporting**: Charts and graphs for admin analytics.
-   [ ] **Docker Support**: Containerization for easy deployment.
-   [ ] **Unit & Integration Tests**: Comprehensive testing suite using xUnit and Moq.
-   [ ] **Profile Management**: Allow users to update their profile and change passwords.
-   [ ] **Search & Filter**: Advanced search capabilities for bootcamps.

## 📝 License

This project is licensed under the MIT License.
