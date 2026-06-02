using Todolist.Application.DTOs;
using Todolist.Domain.Entities;

namespace Todolist.Application.Services;

public interface ITodoService
{
    public Task CreateTodoAsync(CreateTodoRequest todoRequest, int userId);
    public Task<List<Todo>> GetAllTodosByUserIdAsync(int userId);
    public Task<bool> DeleteTodoAsync(int id);
    public Task<Todo> GetTodoByIdAsync(int id);
    public Task UpdateTodoAsync(Todo todo, int id);
}