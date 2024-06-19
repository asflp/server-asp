using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(t => t.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(u => u.CreatedAt).HasDefaultValue(DateTime.Now);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Surname).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.PhoneNumber).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        
        builder.HasMany(u => u.Advertisements).WithOne(a => a.User).HasForeignKey(a => a.UserId);
        builder.HasMany(u => u.Likes).WithOne(l => l.User).HasForeignKey(l => l.UserId);
        builder.HasMany(u => u.Cart).WithMany(a => a.InCart);
    }
}