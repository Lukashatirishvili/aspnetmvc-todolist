using Microsoft.EntityFrameworkCore;

using Todolist.Domain.Entities;
namespace Todolist.Infrastructure.Data;


public class TodolistDbContext :DbContext
{
    public TodolistDbContext(DbContextOptions<TodolistDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Todo>  Todos { get; set; }
    public DbSet<User>  Users { get; set; }
    public DbSet<RegisterUser>  RegisterUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .HasOne(x => x.User)
            .WithMany(x => x.Todos)
            .HasForeignKey(t => t.UserId);
        
        base.OnModelCreating(modelBuilder);
    }
}