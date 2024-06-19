using Microsoft.EntityFrameworkCore;
using SemWorkAsp.DataAccess.Configurations;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Like> Likes { get; set; }

    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
    }
}