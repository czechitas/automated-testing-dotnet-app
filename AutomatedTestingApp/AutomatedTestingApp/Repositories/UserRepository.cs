using AutomatedTestingApp.Entity;
using AutomatedTestingApp.Helpers;

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

    public User GetUserById(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.UserId == userId);
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
        var student = _context.Users.Find(user.UserId);
        
        if(student != null)
            _context.Users.Remove(student);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}