# 🚀 Bootcamp Yönetim Sistemi

Merhaba! Bu proje, yazılım bootcamp'lerinin yönetimini kolaylaştırmak için tasarlanmış kapsamlı bir .NET 8 uygulamasıdır. Eğitmenler, başvuru sahipleri, çalışanlar, bootcamp'ler ve başvurular arasındaki ilişkileri yönetmeye yardımcı olur.

## 📋 Özellikler

- **Kullanıcı Yönetimi**
  - Başvuru sahipleri, eğitmenler ve çalışanlar için ayrı roller
  - JWT tabanlı kimlik doğrulama ve yetkilendirme
  - Güvenli şifre hashleme

- **Bootcamp Yönetimi**
  - Bootcamp oluşturma, düzenleme ve silme
  - Eğitmenlerle bootcamp'leri ilişkilendirme
  - Bootcamp durumlarını yönetme (hazırlık, başvuruya açık, devam ediyor, tamamlandı)

- **Başvuru İşlemleri**
  - Bootcamp'lere başvuru yapma
  - Başvuru durumlarını takip etme (beklemede, kabul edildi, reddedildi)
  - Kara listeye alınmış başvuru sahiplerini engelleme

- **Kara Liste Yönetimi**
  - Problemli başvuru sahiplerini kara listeye alma
  - Kara listedeki kişilerin başvurularını engelleme

## 🏗️ Mimari

Proje, temiz mimari prensiplerini takip eden katmanlı bir yapıya sahiptir:

### 1. Entities Katmanı
- Temel veri modellerini içerir (User, Applicant, Instructor, Employee, BootcampEntity, Application, Blacklist)
- Enum değerleri (ApplicationState, BootcampState)

### 2. Core Katmanı
- Generic repository arayüzleri
- İş kuralları için exception sınıfları
- Güvenlik bileşenleri (JWT, Hashing)
- Global exception middleware

### 3. Repositories Katmanı
- Entity Framework Core implementasyonları
- Fluent API ile veritabanı konfigürasyonu
- Unit of Work pattern

### 4. Business Katmanı
- DTO'lar (Request/Response)
- Servis arayüzleri ve implementasyonları
- AutoMapper profilleri
- İş kuralları

### 5. WebAPI Katmanı
- REST API controller'ları
- Swagger entegrasyonu
- JWT konfigürasyonu

## 🛠️ Teknolojiler

- **.NET 8**: En son .NET sürümü ile geliştirilmiştir
- **Entity Framework Core 8**: ORM aracı olarak kullanılmıştır
- **SQL Server**: Veritabanı olarak kullanılmıştır
- **JWT**: Kimlik doğrulama için JSON Web Token kullanılmıştır
- **AutoMapper**: Nesneler arası dönüşüm için kullanılmıştır
- **Swagger**: API dokümantasyonu için kullanılmıştır

## 🚀 Başlangıç

### Gereksinimler
- .NET 8 SDK
- SQL Server (LocalDB veya Express)
- Bir IDE (Visual Studio, VS Code, Rider vb.)

### Kurulum

1. Repo'yu klonlayın:
   ```
   git clone https://github.com/batuhansimsar/Bootcamp.git
   ```
   
2. Sonrasında gerekli ayarları yapın. WebAPI klasöründe bulunan Program.cs dosyasındaki SQL Server bağlantı bilgilerini kendi bilgisayarınıza göre düzenleyin. Ayrıca appsettings.json dosyasındaki ayarları da buna uygun şekilde güncelleyin.

3. Proje dizinine gidin:
   ```
   cd Bootcamp
   ```

4. Bağımlılıkları yükleyin:
   ```
   dotnet restore
   ```

5. Veritabanını oluşturun:
   ```
   dotnet ef database update --project Bootcamp.Repositories --startup-project Bootcamp.WebAPI
   ```

6. Uygulamayı çalıştırın:
   ```
   cd Bootcamp.WebAPI
   dotnet run
   ```

7. Tarayıcınızda Swagger UI'a erişin:
   ```
   http://localhost:5158/swagger
   ```

## 🔍 API Kullanımı

### Kimlik Doğrulama

#### Kayıt Olma
```http
POST /api/Auth/register/applicant
POST /api/Auth/register/instructor
POST /api/Auth/register/employee
```

Örnek istek:
```json
{
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "dateOfBirth": "1990-01-01",
  "nationalityIdentity": "12345678901",
  "email": "ahmet@example.com",
  "password": "Password123",
  "about": "Yazılım geliştirmeye meraklıyım." // Sadece başvuru sahipleri için
}
```

#### Giriş Yapma
```http
POST /api/Auth/login
```

Örnek istek:
```json
{
  "email": "ahmet@example.com",
  "password": "Password123"
}
```

### Bootcamp İşlemleri

#### Bootcamp Oluşturma
```http
POST /api/Bootcamps
```

Örnek istek:
```json
{
  "name": ".NET Core Bootcamp",
  "instructorId": 1,
  "startDate": "2023-08-01",
  "endDate": "2023-10-30"
}
```

#### Tüm Bootcamp'leri Listeleme
```http
GET /api/Bootcamps
```

### Başvuru İşlemleri

#### Başvuru Yapma
```http
POST /api/Applications
```

Örnek istek:
```json
{
  "applicantId": 1,
  "bootcampId": 1
}
```

#### Başvuruları Listeleme
```http
GET /api/Applications
```

## 💡 İş Kuralları

- Kara listeye alınmış bir başvuru sahibi bootcamp'lere başvuramaz
- Bir başvuru sahibi aynı bootcamp'e birden fazla başvuru yapamaz
- Bootcamp başlangıç tarihi, bitiş tarihinden önce olmalıdır
- Bootcamp'in durumu "başvuruya açık" olmadığında başvuru yapılamaz


⭐️ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın! ⭐️ 
