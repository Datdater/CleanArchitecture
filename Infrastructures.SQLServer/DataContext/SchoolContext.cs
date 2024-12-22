using Entities;
using Infrastructures.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.SQLServer.DataContext;
public class SchoolContext: DbContext
{
    public DbSet<StudentEntity> Students { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username).IsUnique();
    }
    
}