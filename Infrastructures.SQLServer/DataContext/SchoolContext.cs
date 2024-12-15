using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.SQLServer.DataContext;
public class SchoolContext: DbContext
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<StudentEntity> Students { get; set; }
    
    protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    
}