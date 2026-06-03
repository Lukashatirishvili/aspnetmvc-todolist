using Todolist.Domain.Entities;
using Todolist.Domain.Interfaces;
using Todolist.Application.DTOs;

namespace Todolist.Application.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    
    public async Task CreateTodoAsync(CreateTodoRequest todoRequest, int userId)
    {
        var todo = new Todo
        {
            TodoText = todoRequest.TodoText,
            UserId = userId,
            IsCompleted = todoRequest.IsCompleted
        };
        
        await _todoRepository.AddTodoAsync(todo);
        await _todoRepository.SaveChangesAsync();
    }

    public async Task<List<Todo>> GetAllTodosByUserIdAsync(int userId)
    {
        return await _todoRepository.GetAllTodosByUserIdAsync(userId);
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var todo = await _todoRepository.GetTodoByIdAsync(id);

        if (todo == null)
        {
            return false;
        }

        await _todoRepository.DeleteTodoAsync(id);

        return true;
    }

    public async Task<Todo> GetTodoByIdAsync(int id)
    {
        await _todoRepository.GetTodoByIdAsync(id);
        return await _todoRepository.GetTodoByIdAsync(id);
    }

    public async Task UpdateTodoAsync(Todo todo, int id)
    {
        todo.UserId = id;
        await _todoRepository.UpdateTodoAsync(todo);
        await _todoRepository.SaveChangesAsync();
    }

    public Todo GetTodoByIdAsNoTracking(int id)
    {
        return _todoRepository.GetTodoByIdAsNoTracking(id);
    }

    public async Task TodoCompletionAsync(Todo todo, int? userId, bool action)
    {
        var temp = new Todo
        {
            Id = todo.Id,
            TodoText = todo.TodoText,
            IsCompleted = action,
            UserId = userId.Value
            
        };
        
        await _todoRepository.UpdateTodoAsync(temp);
        await _todoRepository.SaveChangesAsync();
    }
}