using Bootcamp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bootcamp.Repositories.Configurations
{
    public class BlacklistConfiguration : IEntityTypeConfiguration<Blacklist>
    {
        public void Configure(EntityTypeBuilder<Blacklist> builder)
        {
            builder.ToTable("Blacklists");
            
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Reason)
                .IsRequired()
                .HasMaxLength(500);
                
            builder.Property(b => b.Date)
                .IsRequired();
                
            // Navigation property
            builder.HasOne(b => b.Applicant)
                .WithMany()
                .HasForeignKey(b => b.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Unique constraint for applicant (only one active blacklist entry per applicant)
            builder.HasIndex(b => b.ApplicantId)
                .IsUnique();
        }
    }
} 