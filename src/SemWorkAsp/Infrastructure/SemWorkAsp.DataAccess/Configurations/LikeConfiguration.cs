using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("likes").HasKey(t => new { t.UserId, t.AdvertisementId });
        builder.HasOne(l => l.User)
            .WithMany(u => u.Likes).HasForeignKey(l => l.UserId);
        builder.HasOne(l => l.Advertisement)
            .WithMany(a => a.Likes).HasForeignKey(l => l.AdvertisementId);
    }
}