using AutomatedTestingApp.Entity;
using AutomatedTestingApp.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Repositories;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public IEnumerable<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetUserById(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId);
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
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