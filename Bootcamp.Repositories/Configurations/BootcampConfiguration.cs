using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repositories.Configurations
{
    public class BootcampConfiguration : IEntityTypeConfiguration<BootcampEntity>
    {
        public void Configure(EntityTypeBuilder<BootcampEntity> builder)
        {
            builder.ToTable("Bootcamps");
            
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(b => b.StartDate)
                .IsRequired();
                
            builder.Property(b => b.EndDate)
                .IsRequired();
                
            builder.Property(b => b.BootcampState)
                .IsRequired()
                .HasConversion<string>();
                
            // Navigation properties
            builder.HasOne(b => b.Instructor)
                .WithMany()
                .HasForeignKey(b => b.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(b => b.Applications)
                .WithOne(a => a.Bootcamp)
                .HasForeignKey(a => a.BootcampId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Unique constraint for bootcamp name
            builder.HasIndex(b => b.Name)
                .IsUnique();
        }
    }
} 