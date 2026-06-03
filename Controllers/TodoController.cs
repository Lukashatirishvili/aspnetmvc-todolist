using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.Application.Services;
using Todolist.Application.DTOs;
using Todolist.Domain.Entities;
using Todolist.Domain.Interfaces;
using Todolist.Infrastructure.Data;


namespace TodolistWebApp.Controllers;

public class TodoController : Controller
{
    private readonly ITodoRepository _todoRepository;
    private readonly TodolistDbContext _context;
    private readonly ITodoService _todoService;

    public TodoController(ITodoRepository todoRepository, TodolistDbContext context, ITodoService todoService)
    {
        _todoRepository = todoRepository;
        _context = context;
        _todoService = todoService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "User");
        }
        
        var todos = await _todoService.GetAllTodosByUserIdAsync(userId.Value);
        
        return View(todos);
    }

    
    public IActionResult CreateTodo()
    {
        
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "User");
        }
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // This protects against CSRF attacks
    public async Task<IActionResult> CreateTodo(CreateTodoRequest todoRequest)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null) return RedirectToAction("Login", "User");
        
        if (!ModelState.IsValid) return View(todoRequest);
        
        await _todoService.CreateTodoAsync(todoRequest, userId.Value);
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTodo(int id)
    {

        var result = await _todoService.DeleteTodoAsync(id);

        if (!result)
        {
            return NotFound();
        }
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditTodo(int id)
    {
        
        var todo = await _todoService.GetTodoByIdAsync(id);
        

        return View(todo);
    }

    [HttpPost]
    public async Task<IActionResult> EditTodo(int id, Todo todo)
    {
        var userId = HttpContext.Session.GetInt32("UserId").Value;

        
        await _todoService.UpdateTodoAsync(todo, userId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UndoCompletion(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var todo = _todoService.GetTodoByIdAsNoTracking(id);

        if (todo == null)
        {
            return NotFound();
        }
        
        await _todoService.TodoCompletionAsync(todo, userId, false);

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Completion(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var todo = _todoService.GetTodoByIdAsNoTracking(id);

        if (todo == null)
        {
            return NotFound();
        }
        
        await _todoService.TodoCompletionAsync(todo, userId, true);

        return RedirectToAction("Index");
    }
}