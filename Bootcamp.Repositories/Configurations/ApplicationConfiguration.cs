using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repositories.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Applications");
            
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.ApplicationState)
                .IsRequired()
                .HasConversion<string>();
                
            // Navigation properties
            builder.HasOne(a => a.Applicant)
                .WithMany(app => app.Applications)
                .HasForeignKey(a => a.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(a => a.Bootcamp)
                .WithMany(b => b.Applications)
                .HasForeignKey(a => a.BootcampId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 