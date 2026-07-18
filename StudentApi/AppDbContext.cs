using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // This creates a "Students" table in your SQL database based on your Student model
    public DbSet<Student> Students { get; set; }
}