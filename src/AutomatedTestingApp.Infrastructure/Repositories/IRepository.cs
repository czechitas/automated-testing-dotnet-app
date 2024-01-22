using System.Linq.Expressions;

namespace AutomatedTestingApp.Infrastructure.Repositories;

public interface IRepository<TEntity>
{
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

    TEntity GetById(object id);

    void Insert(TEntity entity);

    void Update(TEntity entity);

    void Delete(object id);

    void Delete(TEntity entityToDelete);
}