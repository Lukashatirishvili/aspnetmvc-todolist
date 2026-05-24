using System.ComponentModel.DataAnnotations.Schema;

namespace TodolistWebApp.Models;

public class Todo
{
    public int Id { get; set; }
    public string TodoText { get; set; }
    public bool IsCompleted { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
}
