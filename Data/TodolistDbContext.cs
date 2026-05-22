using Microsoft.EntityFrameworkCore;
using TodolistWebApp.Models;

namespace TodolistWebApp.Data;

public class TodolistDbContext :DbContext
{
    public TodolistDbContext(DbContextOptions<TodolistDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Todo>  Todos { get; set; }
}