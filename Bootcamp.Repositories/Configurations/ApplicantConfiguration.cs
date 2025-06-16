using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repositories.Configurations
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.Property(a => a.About)
                .HasMaxLength(500);
                
            // Navigation properties
            builder.HasMany(a => a.Applications)
                .WithOne(app => app.Applicant)
                .HasForeignKey(app => app.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany<Blacklist>()
                .WithOne(b => b.Applicant)
                .HasForeignKey(b => b.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 