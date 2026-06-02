
using Todolist.Domain.Entities;

namespace Todolist.Domain.Interfaces;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllTodosAsync();
    Task<List<Todo>> GetAllTodosByUserIdAsync(int userId);
    Task<Todo> GetTodoByIdAsync(int id);
    Task AddTodoAsync(Todo task);
    Task DeleteTodoAsync(int id);
    Task UpdateTodoAsync(Todo todo);
    Task SaveChangesAsync();
}


