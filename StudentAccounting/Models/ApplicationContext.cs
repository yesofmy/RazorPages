using Microsoft.EntityFrameworkCore;

namespace StudentAccounting.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
}