using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodolistWebApp.Data;
using TodolistWebApp.Models;

namespace TodolistWebApp.Controllers;

public class TodoController : Controller
{
    private readonly TodolistDbContext _context;

    public TodoController(TodolistDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "User");
        }
        
        var todos = _context.Todos.Where(x => x.UserId == userId).ToList();
        
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
    public IActionResult CreateTodo(Todo todo)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "User");
        }
        
        todo.UserId = userId.Value;
        var temp = todo;
        
        _context.Todos.Add(temp);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DeleteTodo(int id)
    {
        var todo = _context.Todos.FirstOrDefault(x => x.Id == id);

        if (todo == null)
        {
            return NotFound();
        }
        
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditTodo(int id)
    {
        
        var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
        

        return View(todo);
    }

    [HttpPost]
    public IActionResult EditTodo(int id, Todo todo)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        todo.UserId = userId.Value;
        _context.Todos.Update(todo);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UndoCompletion(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        var todo = _context.Todos.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        var temp = new Todo
        {
            Id = todo.Id,
            TodoText = todo.TodoText,
            IsCompleted = false,
            UserId = userId.Value

        };
            
        _context.Todos.Update(temp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Completion(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var todo = _context.Todos.AsNoTracking().FirstOrDefault(x => x.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        var temp = new Todo
        {
            Id = todo.Id,
            TodoText = todo.TodoText,
            IsCompleted = true,
            UserId = userId.Value
            
        };
            
        _context.Todos.Update(temp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}