using Microsoft.AspNetCore.Mvc;
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
        var todos = _context.Todos.ToList();
        
        return View(todos);
    }

    
    public IActionResult CreateTodo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateTodo(Todo todo)
    {
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
}