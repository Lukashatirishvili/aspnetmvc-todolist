using Microsoft.AspNetCore.Mvc;

namespace TodolistWebApp.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}