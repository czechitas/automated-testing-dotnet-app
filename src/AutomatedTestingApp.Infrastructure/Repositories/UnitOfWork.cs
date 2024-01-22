namespace AutomatedTestingApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private Dictionary<Type, Object> _repositories;
    
    public UnitOfWork(DataContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }
    
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
        // TODO rollback changes here
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.Keys.Contains(typeof(TEntity)))
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        var repository = new Repository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}