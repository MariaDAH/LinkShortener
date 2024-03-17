using LinkShortener.Infrastructure.Repositories;

namespace LinkShortener.Application.Services;

public interface IUnitOfWork: IDisposable
{
    void Commit();
    void Rollback();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}