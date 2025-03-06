using Business.Abstractions;
using Business.Products.Queries.GetById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Queries.GetAll;

internal sealed class GetAllProductsQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse?>>
{
    public async Task<IEnumerable<ProductResponse?>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) =>
        await dbContext.Products
            .Select(x => new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price))
            .ToListAsync(cancellationToken);
}