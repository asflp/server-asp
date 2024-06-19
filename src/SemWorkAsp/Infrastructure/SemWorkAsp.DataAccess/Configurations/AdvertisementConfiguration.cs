using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess.Configurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.ToTable("advertisements").HasKey(t => t.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(a => a.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Category).IsRequired();
        builder.Property(a => a.Price).IsRequired();
        builder.Property(a => a.Description).HasMaxLength(1000).IsRequired();
        builder.Property(a => a.City).IsRequired();
        
        builder.HasOne(a => a.User).WithMany(u => u.Advertisements).HasForeignKey(a => a.UserId);
        builder.HasMany(a => a.Likes).WithOne(l => l.Advertisement).HasForeignKey(l => l.AdvertisementId);
        builder.HasMany(a => a.InCart).WithMany(u => u.Cart);
    }
}