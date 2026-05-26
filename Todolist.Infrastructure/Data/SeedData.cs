/* using Microsoft.EntityFrameworkCore;
using TodolistWebApp.Models;

namespace TodolistWebApp.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new TodolistDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<TodolistDbContext>>());
        if (context.Todos.Any())
        {
            return;
        }

        context.Todos.AddRange(
            new Todo
            {
                TodoText = "Workout",
                IsCompleted = false,
            },
            new Todo
            {
                TodoText = "Breakfast",
                IsCompleted = false,
            },
            new Todo
            {
                TodoText = "Study",
                IsCompleted = false,
            },
            new Todo
            {
                TodoText = "Reading",
                IsCompleted = false,
            }
        );
        context.SaveChanges();
    }
}

*/