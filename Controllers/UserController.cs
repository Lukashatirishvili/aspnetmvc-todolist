using Microsoft.AspNetCore.Mvc;
using TodolistWebApp.Data;
using TodolistWebApp.Models;
using TodolistWebApp.ViewModels;

namespace TodolistWebApp.Controllers;

public class UserController : Controller
{
    private readonly TodolistDbContext _context;

    public UserController(TodolistDbContext context)
    {
        _context = context;
    }
    
    // GET
    public IActionResult Login()
    {
        
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }
        
        var userExist = _context.Users.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password);

        if (userExist == null)
        {
            ModelState.AddModelError("", "Email or password is incorrect");
            return View(userExist);
        }
        
        HttpContext.Session.SetInt32("UserId", userExist.Id);
        
        return RedirectToAction("Index", "Todo");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User user)
    {

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        if (_context.Users.Any(u => u.Email == user.Email) || _context.Users.Any(u => u.UserName == user.UserName))
        {
            ModelState.AddModelError("", "Email or Username is already taken"); 
            return View(user);
        }
        
        
        _context.Users.Add(user);
        _context.SaveChanges();
        HttpContext.Session.SetInt32("UserId", user.Id);
        
        return RedirectToAction("Index", "Todo");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Index", "Todo");
    }
}