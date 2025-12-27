# 🚀 Bootcamp Yönetim Sistemi

Yazılım bootcamp'lerinin yönetimini kolaylaştırmak için tasarlanmış kapsamlı bir .NET 8 Web API uygulamasıdır. Eğitmenler, başvuru sahipleri, çalışanlar, bootcamp'ler ve başvurular arasındaki ilişkileri yönetir.

## 📋 Özellikler

### Kullanıcı Yönetimi
- Başvuru sahipleri (Applicant), eğitmenler (Instructor) ve çalışanlar (Employee) için ayrı roller
- JWT tabanlı kimlik doğrulama ve yetkilendirme
- Güvenli şifre hashleme (HMACSHA512)
- Rate limiting ile brute force koruması

### Bootcamp Yönetimi
- Bootcamp CRUD işlemleri
- Eğitmenlerle bootcamp ilişkilendirme
- Bootcamp durumları: `Preparing`, `Open`, `Started`, `Completed`

### Başvuru İşlemleri
- Bootcamp başvuruları
- Başvuru durumları: `Pending`, `Accepted`, `Rejected`
- Kara liste kontrolü

### Kara Liste Yönetimi
- Problemli başvuru sahiplerini engelleme
- Otomatik başvuru reddi

## 🏗️ Proje Yapısı

```
Bootcamp/
├── Bootcamp.Entities/          # Domain modelleri
│   ├── User.cs                 # Base kullanıcı sınıfı
│   ├── Applicant.cs            # Başvuru sahibi
│   ├── Instructor.cs           # Eğitmen
│   ├── Employee.cs             # Çalışan
│   ├── BootcampEntity.cs       # Bootcamp modeli
│   ├── Application.cs          # Başvuru modeli
│   └── Blacklist.cs            # Kara liste
│
├── Bootcamp.Core/              # Ortak bileşenler
│   ├── Repositories/           # Generic repository arayüzleri
│   ├── Security/               # JWT & Hashing
│   ├── Middleware/             # Global exception handler
│   ├── Exceptions/             # Custom exception sınıfları
│   └── UnitOfWork/             # Unit of Work pattern
│
├── Bootcamp.Repositories/      # Veritabanı katmanı
│   ├── BootcampDbContext.cs    # EF Core DbContext
│   └── *Repository.cs          # Repository implementasyonları
│
├── Bootcamp.Business/          # İş mantığı
│   ├── DTOs/                   # Request/Response modelleri
│   ├── Services/               # Servis implementasyonları
│   ├── Rules/                  # İş kuralları
│   └── Profiles/               # AutoMapper profilleri
│
└── Bootcamp.WebAPI/            # API katmanı
    ├── Controllers/            # REST endpoints
    └── Program.cs              # Uygulama konfigürasyonu
```

## 🛠️ Teknolojiler

| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| .NET | 8.0 | Framework |
| Entity Framework Core | 8.0 | ORM |
| SQL Server | 2022 | Veritabanı |
| JWT Bearer | - | Authentication |
| AutoMapper | - | Object mapping |
| Serilog | - | Logging |
| Swagger | - | API dokümantasyonu |

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server (veya Docker)
- IDE (Visual Studio, VS Code, Rider)

### Option 1: Docker ile Çalıştırma (Önerilen)

```bash
# Repo'yu klonlayın
git clone https://github.com/batuhansimsar/Bootcamp.git
cd Bootcamp

# Docker Compose ile başlatın
docker-compose up -d
```

API: `http://localhost:5158/swagger`

### Option 2: Manuel Kurulum

```bash
# Repo'yu klonlayın
git clone https://github.com/batuhansimsar/Bootcamp.git
cd Bootcamp

# Bağımlılıkları yükleyin
dotnet restore

# appsettings.json'daki connection string'i düzenleyin
# Veritabanını oluşturun
dotnet ef database update --project Bootcamp.Repositories --startup-project Bootcamp.WebAPI

# Uygulamayı çalıştırın
cd Bootcamp.WebAPI
dotnet run
```

API: `http://localhost:5158/swagger`

## 🔍 API Endpoints

### Auth
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| POST | `/api/Auth/login` | Giriş yap |
| POST | `/api/Auth/register/applicant` | Başvuru sahibi kayıt |
| POST | `/api/Auth/register/instructor` | Eğitmen kayıt |
| POST | `/api/Auth/register/employee` | Çalışan kayıt |

### Bootcamps
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/Bootcamps` | Tüm bootcamp'leri listele |
| GET | `/api/Bootcamps/{id}` | Bootcamp detayı |
| POST | `/api/Bootcamps` | Yeni bootcamp oluştur |
| PUT | `/api/Bootcamps/{id}` | Bootcamp güncelle |
| DELETE | `/api/Bootcamps/{id}` | Bootcamp sil |

### Applications
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/Applications` | Başvuruları listele |
| POST | `/api/Applications` | Yeni başvuru |
| PUT | `/api/Applications/{id}` | Başvuru güncelle |
| DELETE | `/api/Applications/{id}` | Başvuru sil |

### Blacklists
| Method | Endpoint | Açıklama |
|--------|----------|----------|
| GET | `/api/Blacklists` | Kara listeyi görüntüle |
| POST | `/api/Blacklists` | Kara listeye ekle |
| DELETE | `/api/Blacklists/{id}` | Kara listeden çıkar |

## 📝 Örnek İstekler

### Kayıt Olma
```json
POST /api/Auth/register/applicant
{
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "dateOfBirth": "1995-01-15",
  "nationalityIdentity": "12345678901",
  "email": "ahmet@example.com",
  "password": "SecurePassword123",
  "about": "Yazılım geliştirici olmak istiyorum"
}
```

### Giriş Yapma
```json
POST /api/Auth/login
{
  "email": "ahmet@example.com",
  "password": "SecurePassword123"
}
```

### Bootcamp Oluşturma
```json
POST /api/Bootcamps
{
  "name": ".NET Core Bootcamp",
  "instructorId": 1,
  "startDate": "2025-03-01",
  "endDate": "2025-05-30"
}
```

## 💡 İş Kuralları

- ❌ Kara listedeki kullanıcılar başvuru yapamaz
- ❌ Aynı bootcamp'e birden fazla başvuru yapılamaz
- ❌ Başlangıç tarihi bitiş tarihinden sonra olamaz
- ❌ Sadece "Open" durumundaki bootcamp'lere başvuru yapılabilir

## 🔒 Güvenlik

- **JWT Authentication**: Token tabanlı kimlik doğrulama
- **Password Hashing**: HMACSHA512 ile şifreleme
- **Rate Limiting**: Login endpoint'i için dakikada 5 istek limiti
- **Global Exception Handling**: Hassas hata bilgilerini gizleme

## 📁 Docker Konfigürasyonu

Proje, SQL Server ile birlikte Docker üzerinde çalışmaya hazırdır:

```yaml
# docker-compose.yml
services:
  bootcamp-api:     # .NET 8 API (port: 5158)
  sqlserver:        # SQL Server 2022 Express (port: 1433)
```

---

<p align="center">
  ⭐️ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın! ⭐️
</p>
