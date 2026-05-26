
using Todolist.Domain.Entities;

namespace Todolist.Domain.Interfaces;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllTodosAsync();
    Task<Todo> GetTodoByIdAsync(int id);
    Task AddAsync(Todo task);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
