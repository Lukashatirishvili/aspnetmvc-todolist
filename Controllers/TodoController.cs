using Microsoft.AspNetCore.Mvc;
using TodolistWebApp.Data;

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
    
    
    
}