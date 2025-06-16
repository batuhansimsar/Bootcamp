using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repositories.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);
                
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.NationalityIdentity)
                .IsRequired()
                .HasMaxLength(20);
                
            builder.Property(u => u.DateOfBirth)
                .IsRequired();
                
            builder.Property(u => u.PasswordHash)
                .IsRequired();
                
            builder.Property(u => u.PasswordSalt)
                .IsRequired();
                
            // Discriminator for TPH (Table Per Hierarchy)
            builder.HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Instructor>("Instructor")
                .HasValue<Applicant>("Applicant")
                .HasValue<Employee>("Employee");
        }
    }
} 