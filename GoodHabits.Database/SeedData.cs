using GoodHabits.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHabits.Database;
public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Habit>().HasData(
            new Habit { Id = 100, Name = "Exercise", Description = "Do some exercise" },
            new Habit { Id = 101, Name = "Read", Description = "Read a book" },
            new Habit { Id = 102, Name = "Meditate", Description = "Meditate for 10 minutes" }
        );
    }
}

