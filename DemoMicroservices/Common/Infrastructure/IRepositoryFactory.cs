namespace Common.Infrastructure;

public interface IRepositoryFactory
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}