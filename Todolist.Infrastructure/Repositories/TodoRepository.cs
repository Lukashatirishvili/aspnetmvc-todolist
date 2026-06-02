using Microsoft.EntityFrameworkCore;
using Todolist.Domain.Entities;
using Todolist.Domain.Interfaces;
using Todolist.Infrastructure.Data;

namespace Todolist.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodolistDbContext _context;

    public TodoRepository(TodolistDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Todo>> GetAllTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<List<Todo>> GetAllTodosByUserIdAsync(int userId)
    {
        return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Todo> GetTodoByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task AddTodoAsync(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
    }

    public async Task DeleteTodoAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}