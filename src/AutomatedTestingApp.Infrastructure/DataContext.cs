using AutomatedTestingApp.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<IdentityUser> Users { get; set; }
}