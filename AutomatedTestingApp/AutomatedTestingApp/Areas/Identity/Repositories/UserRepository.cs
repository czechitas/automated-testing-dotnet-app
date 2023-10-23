using AutomatedTestingApp.Areas.Identity.Models;
using AutomatedTestingApp.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Areas.Identity.Repositories;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public IEnumerable<IdentityUser> GetUsers()
    {
        return _context.Users.ToList();
    }

    public IdentityUser? GetUserById(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId);
    }

    public Task<IdentityUser?> GetUserByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public void CreateUser(IdentityUser user)
    {
        _context.Users.Add(user);
    }

    public void UpdateUser(IdentityUser user)
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(IdentityUser user)
    {
        var student = GetUserById(user.UserId);
        
        if(student != null)
            _context.Users.Remove(student);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}