using AutomatedTestingApp.Areas.Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    public DbSet<IdentityUser> Users { get; set; }
}