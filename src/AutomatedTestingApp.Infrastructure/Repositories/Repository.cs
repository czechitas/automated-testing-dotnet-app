using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingApp.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DataContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
    
    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = _dbSet;
        
        if (filter != null)
            query = query.Where(filter);
        
        if (orderBy != null)
            return orderBy(query).ToList();
        
        return _dbSet.ToList();
    }
    
    public TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }
    
    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
    }
    
    public void Delete(object id)
    {
        TEntity entityToDelete = _dbSet.Find(id);
        
        if (entityToDelete == null)
            return;
        
        Delete(entityToDelete);
    }
    
    public void Delete(TEntity entityToDelete)
    {
        if (_dbSet.Local.Contains(entityToDelete))
            _dbSet.Remove(entityToDelete);
        else
            _dbSet.Attach(entityToDelete);
    }
}
