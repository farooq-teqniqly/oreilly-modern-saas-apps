using GoodHabits.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHabits.Database;

public class GoodHabitsDbContext: DbContext
{
    public DbSet<Habit>? Habits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=sqlserver;Database=GoodHabits;User Id=sa;Password=P@ssword1!;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => SeedData.Seed(modelBuilder);

}
