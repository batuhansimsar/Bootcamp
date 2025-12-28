# 🚀 Bootcamp Management System

A comprehensive full-stack application for managing software bootcamp programs, designed to streamline the process for applicants, instructors, and employees.

## 🌟 Features

### Backend (.NET 8 Web API)
- **JWT Authentication & Authorization** - Secure token-based authentication
- **Role-Based Access Control** - Separate roles for Applicants, Instructors, and Employees
- **Bootcamp Management** - Full CRUD operations with state management (Preparing, Open, Started, Completed)
- **Application System** - Track bootcamp applications with status management (Pending, Accepted, Rejected)
- **Blacklist System** - Prevent problematic applicants from applying
- **Rate Limiting** - Built-in brute force protection
- **Global Exception Handling** - Secure error management
- **Logging** - Comprehensive logging with Serilog

### Frontend (React + TypeScript)
- **Modern UI/UX** - Beautiful glassmorphism design with dark mode
- **Responsive Design** - Works seamlessly on all devices
- **Role-Based Registration** - Separate registration flows for different user types
- **Real-time Dashboard** - View available bootcamps with live status updates
- **Smooth Animations** - Engaging micro-interactions and transitions
- **Type Safety** - Full TypeScript support for better development experience

## 🏗️ Project Structure

```
Bootcamp/
├── Backend (.NET 8)
│   ├── Bootcamp.Entities/       # Domain models
│   ├── Bootcamp.Core/           # Core components (Security, Middleware, UnitOfWork)
│   ├── Bootcamp.Repositories/   # Data access layer with EF Core
│   ├── Bootcamp.Business/       # Business logic & services
│   └── Bootcamp.WebAPI/         # REST API endpoints
│
└── Frontend (React + TypeScript)
    ├── src/
    │   ├── components/          # React components (Login, Register, Dashboard)
    │   ├── api.ts               # API service layer
    │   ├── AuthContext.tsx      # Authentication state management
    │   └── index.css            # Design system & utilities
    └── index.html
```

## 🛠️ Technologies

### Backend Stack
| Technology | Description |
|-----------|-------------|
| .NET 8 | Modern framework with minimal APIs |
| Entity Framework Core 8 | ORM for database operations |
| SQL Server | Relational database |
| JWT Bearer | Token-based authentication |
| AutoMapper | Object-to-object mapping |
| Serilog | Structured logging |
| Swagger/OpenAPI | API documentation |

### Frontend Stack
| Technology | Description |
|-----------|-------------|
| React 18 | UI library |
| TypeScript | Type-safe JavaScript |
| Vite | Fast build tool & dev server |
| React Router | Client-side routing |
| Axios | HTTP client |
| CSS3 | Modern styling with variables |

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+ and npm
- SQL Server (LocalDB or Express)

### Backend Setup

```bash
# Navigate to project root
cd Bootcamp

# Restore dependencies
dotnet restore

# Update connection string in Bootcamp.WebAPI/appsettings.json
# Example:
# "ConnectionStrings": {
#   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BootcampDb;Trusted_Connection=true;"
# }

# Create database
dotnet ef database update --project Bootcamp.Repositories --startup-project Bootcamp.WebAPI

# Run the API
cd Bootcamp.WebAPI
dotnet run
```

API will be available at: `http://localhost:5158/swagger`

### Frontend Setup

```bash
# Navigate to frontend directory
cd Bootcamp/frontend

# Install dependencies
npm install

# Run development server
npm run dev
```

Frontend will be available at: `http://localhost:5173`

## 📝 API Endpoints

### Authentication
- `POST /api/Auth/login` - User login
- `POST /api/Auth/register/applicant` - Register as applicant
- `POST /api/Auth/register/instructor` - Register as instructor
- `POST /api/Auth/register/employee` - Register as employee

### Bootcamps
- `GET /api/Bootcamps` - List all bootcamps
- `GET /api/Bootcamps/{id}` - Get bootcamp details
- `POST /api/Bootcamps` - Create new bootcamp
- `PUT /api/Bootcamps/{id}` - Update bootcamp
- `DELETE /api/Bootcamps/{id}` - Delete bootcamp

### Applications
- `GET /api/Applications` - List applications
- `POST /api/Applications` - Submit application
- `PUT /api/Applications/{id}` - Update application
- `DELETE /api/Applications/{id}` - Delete application

### Blacklist
- `GET /api/Blacklists` - View blacklist
- `POST /api/Blacklists` - Add to blacklist
- `DELETE /api/Blacklists/{id}` - Remove from blacklist

## 🎨 Design Features

- **Glassmorphism UI** - Modern frosted glass effect
- **Dark Mode** - Eye-friendly dark color scheme
- **Gradient Accents** - Vibrant purple and blue gradients
- **Micro-animations** - Smooth hover effects and transitions
- **Responsive Grid** - Adaptive layouts for all screen sizes
- **Custom Scrollbars** - Styled to match the dark theme

## 🔒 Security Features

- **JWT Authentication** - Secure token-based auth
- **Password Hashing** - HMACSHA512 encryption
- **Rate Limiting** - 5 requests/minute for login, 100 requests/minute general
- **Global Exception Handler** - Prevents sensitive data leakage
- **Input Validation** - Comprehensive data validation

## 💡 Business Rules

- ❌ Blacklisted users cannot apply to bootcamps
- ❌ Users cannot apply to the same bootcamp multiple times
- ❌ Start date must be before end date
- ✅ Only "Open" status bootcamps accept applications

## 📸 Screenshots

*(Screenshots would be placed here showing login, dashboard, and bootcamp cards)*

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is open-source and available under the MIT License.

---

<p align="center">
  ⭐️ If you like this project, please give it a star! ⭐️
</p>
