using LinkShortener.Infrastructure.Daos;
using LinkShortener.Infrastructure.Repositories;

namespace LinkShortener.Application.Services;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public void Commit()
    {
        context.SaveChanges();
    }

    public void Rollback()
    {
        // Rollback changes if needed
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new Repository<TEntity>(context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public void Dispose()
    {
        context.Dispose();
    }
}