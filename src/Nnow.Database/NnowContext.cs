using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System.Reflection;
using Nnow.Settings;

namespace Nnow.Database;

public class NnowContext : DbContext
{
    readonly IOptions<NnowAppKeys> _keys;

    public NnowContext(IOptions<NnowAppKeys> keys) 
    { 
        _keys = keys;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SqlServerDbContextOptionsBuilder sqlBuilder = new SqlServerDbContextOptionsBuilder(optionsBuilder);
        optionsBuilder.UseSqlServer(_keys.Value.DbConnectionString);
    }

}

