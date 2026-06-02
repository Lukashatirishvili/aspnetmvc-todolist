namespace Todolist.Application.DTOs;

public class CreateTodoRequest
{
    public string TodoText { get; set; }
    public bool IsCompleted { get; set; }
}