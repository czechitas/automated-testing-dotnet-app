using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Core.Users;

[Keyless]
public class IdentityUser
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}