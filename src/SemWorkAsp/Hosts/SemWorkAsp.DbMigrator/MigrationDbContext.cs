using Microsoft.EntityFrameworkCore;
using SemWorkAsp.DataAccess;

namespace SemWorkAsp.DbMigrator;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(DbContextOptions options) : base(options)
    {
    }
}