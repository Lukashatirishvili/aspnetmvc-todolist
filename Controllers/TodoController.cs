using Microsoft.AspNetCore.Mvc;

namespace TodolistWebApp.Controllers;

public class TodoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
}