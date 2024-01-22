using AutomatedTestingApp.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Helpers;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<IdentityUser> Users { get; set; }
}