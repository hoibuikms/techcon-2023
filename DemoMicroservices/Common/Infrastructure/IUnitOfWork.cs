using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure;


public interface IUnitOfWork : IRepositoryFactory, IDisposable
{
    int Commit();
    Task<int> CommitAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}