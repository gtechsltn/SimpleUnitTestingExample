using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}