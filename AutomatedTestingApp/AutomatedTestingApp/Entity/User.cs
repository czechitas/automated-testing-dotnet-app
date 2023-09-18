using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Entity;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}