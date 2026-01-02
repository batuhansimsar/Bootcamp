# 🚀 Bootcamp Management System

A modern, full-stack web application designed to manage technology bootcamps. This platform facilitates the interaction between Applicants, Instructors, and Employees, streamlining the process of bootcamp creation, application, and management.

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)
![React](https://img.shields.io/badge/React-18-blue.svg)
![TypeScript](https://img.shields.io/badge/TypeScript-5.0-blue.svg)

## 🎯 Overview

This Bootcamp Management System provides a comprehensive solution for managing bootcamp programs with role-based access control. The system supports three distinct user roles:

- **👨‍🎓 Applicants**: Browse available bootcamps, submit applications, and track application status
- **👨‍🏫 Instructors**: Create and manage bootcamps, review applications, and manage participants
- **👔 Employees**: Administrative oversight, manage blacklists, and oversee all bootcamp operations

## 🛠️ Technology Stack

This project is built using **Clean Architecture** principles to ensure scalability, maintainability, and testability.

### Backend (.NET 8 Web API)
- **Core:** ASP.NET Core 8
- **Architecture:** Clean Architecture (N-Layer)
  - `Bootcamp.Core` - Business logic and interfaces
  - `Bootcamp.Entities` - Domain entities
  - `Bootcamp.Business` - Services and DTOs
  - `Bootcamp.Repositories` - Data access layer
  - `Bootcamp.WebAPI` - API controllers and configuration
- **Data Access:** Entity Framework Core
- **Database:** PostgreSQL (configurable for SQL Server)
- **Authentication:** JWT (JSON Web Tokens)
- **Logging:** Serilog
- **Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Rate Limiting:** Built-in ASP.NET Core rate limiting

### Frontend (React + TypeScript)
- **Framework:** React 18
- **Build Tool:** Vite
- **Language:** TypeScript
- **Styling:** Modern CSS3 with Glassmorphism design
- **State Management:** Context API for authentication
- **HTTP Client:** Axios
- **Routing:** React Router v6

## ✨ Features

### Authentication & Authorization
- ✅ Secure JWT-based authentication
- ✅ Role-based access control (RBAC)
- ✅ Protected routes and API endpoints
- ✅ Rate limiting on login endpoint

### Bootcamp Management
- ✅ Create, update, delete, and list bootcamps
- ✅ Bootcamp status tracking (Preparing, Open, Started, Completed)
- ✅ Date-based bootcamp filtering
- ✅ Instructor assignment

### Application Process
- ✅ Browse available bootcamps
- ✅ Submit applications
- ✅ Track application status (Pending, Approved, Rejected)
- ✅ Duplicate application prevention
- ✅ Blacklist checking
- ✅ CV/Resume upload for applicants

### User Interface
- ✅ Modern glassmorphism design
- ✅ Responsive layouts for all screen sizes
- ✅ Role-specific dashboards
- ✅ Toast notifications for user feedback
- ✅ Loading states and error handling
- ✅ Smooth animations and transitions

### Admin Features
- ✅ Blacklist management
- ✅ Application status updates
- ✅ Bootcamp participant management
- ✅ View all applications by bootcamp

## 🚀 Getting Started

Follow these instructions to get the project running on your local machine.

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (Latest LTS - v18 or higher)
- PostgreSQL or SQL Server

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/Bootcamp.git
cd Bootcamp
```

### 2. Backend Setup

#### Configure Database Connection

Update the connection string in `Bootcamp.WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=BootcampDb;Username=postgres;Password=yourpassword"
  }
}
```

#### Restore Dependencies

```bash
dotnet restore
```

#### Apply Database Migrations

```bash
dotnet ef database update --project Bootcamp.Repositories --startup-project Bootcamp.WebAPI
```

#### Run the Backend

```bash
dotnet run --project Bootcamp.WebAPI
```

The backend API will start on `http://localhost:5158`

### 3. Frontend Setup

#### Navigate to Frontend Directory

```bash
cd frontend
```

#### Install Dependencies

```bash
npm install
```

#### Configure API URL (if needed)

The API base URL is configured in `frontend/src/api.ts`:

```typescript
const API_BASE_URL = 'http://localhost:5158/api';
```

#### Start Development Server

```bash
npm run dev
```

The frontend will start on `http://localhost:5173`

## 📁 Project Structure

```
Bootcamp/
├── Bootcamp.Core/              # Core business logic and interfaces
├── Bootcamp.Entities/          # Domain entities (User, Bootcamp, Application, etc.)
├── Bootcamp.Business/          # Services, DTOs, and business rules
├── Bootcamp.Repositories/      # Data access layer and DbContext
├── Bootcamp.WebAPI/            # API controllers and configuration
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
└── frontend/                   # React frontend application
    ├── src/
    │   ├── components/         # React components
    │   ├── api.ts             # API client configuration
    │   ├── AuthContext.tsx    # Authentication context
    │   └── App.tsx            # Main application component
    └── package.json
```

## 🔐 API Endpoints

### Authentication
- `POST /api/Auth/login` - User login
- `POST /api/Auth/register/applicant` - Register as applicant
- `POST /api/Auth/register/instructor` - Register as instructor
- `POST /api/Auth/register/employee` - Register as employee

### Bootcamps
- `GET /api/Bootcamps` - Get all bootcamps
- `GET /api/Bootcamps/{id}` - Get bootcamp by ID
- `POST /api/Bootcamps` - Create new bootcamp (Instructor only)
- `PUT /api/Bootcamps/{id}` - Update bootcamp
- `DELETE /api/Bootcamps/{id}` - Delete bootcamp

### Applications
- `GET /api/Applications` - Get all applications
- `GET /api/Applications/{id}` - Get application by ID
- `POST /api/Applications` - Submit new application
- `GET /api/Applications/my-applications/{applicantId}` - Get user's applications
- `GET /api/Applications/bootcamp/{bootcampId}` - Get applications for a bootcamp
- `PATCH /api/Applications/status` - Update application status

### Blacklist
- `GET /api/Blacklists` - Get all blacklisted users
- `POST /api/Blacklists` - Add user to blacklist
- `DELETE /api/Blacklists/{id}` - Remove from blacklist

## 🎨 Design Features

The application features a modern, premium design with:

- **Glassmorphism UI**: Frosted glass effect with backdrop blur
- **Gradient Backgrounds**: Dynamic, vibrant color schemes
- **Smooth Animations**: Micro-interactions and transitions
- **Responsive Design**: Mobile-first approach
- **Dark Theme**: Eye-friendly color palette
- **Custom Components**: Toast notifications, loading spinners, and more

## 🔧 Configuration

### JWT Configuration

Configure JWT settings in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key-here-minimum-32-characters",
    "Issuer": "BootcampAPI",
    "Audience": "BootcampClient",
    "ExpiryMinutes": 60
  }
}
```

### CORS Configuration

CORS is configured in `Program.cs` to allow the frontend origin:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
});
```

## 🧪 Testing

### Manual Testing

1. Register a new user (Applicant, Instructor, or Employee)
2. Login with credentials
3. Navigate through the dashboard
4. Create a bootcamp (as Instructor)
5. Apply to a bootcamp (as Applicant)
6. View and manage applications

## 🔮 Future Improvements

The following features are planned for future releases:

- [ ] **Email Notifications**: Integration with SMTP for registration and application updates
- [ ] **Advanced Reporting**: Charts and graphs for admin analytics
- [ ] **Docker Support**: Containerization for easy deployment
- [ ] **Unit & Integration Tests**: Comprehensive testing suite using xUnit and Moq
- [ ] **Profile Management**: Allow users to update profiles and change passwords
- [ ] **Search & Filter**: Advanced search capabilities for bootcamps
- [ ] **Real-time Notifications**: WebSocket integration for live updates
- [ ] **Multi-language Support**: i18n for internationalization
- [ ] **Export Features**: Export applications and bootcamp data to CSV/PDF

## 🐛 Known Issues & Fixes

### Registration Bug Fix (2026-01-01)
Fixed a critical bug where Instructor and Employee registration was failing with 400 Bad Request errors. The issue was caused by missing `dateOfBirth` and `nationalityIdentity` fields required by the backend's `UserRequestDto` base class. These fields are now properly included for all user types.

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👨‍💻 Author

**Batuhan Simsar**

## 🙏 Acknowledgments

- Built with Clean Architecture principles
- Inspired by modern bootcamp management needs
- Designed with user experience as a priority

---

**Note**: This is a portfolio project demonstrating full-stack development skills with .NET and React.
