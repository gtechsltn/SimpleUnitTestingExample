using Business.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Queries.GetById;

internal sealed class GetProductByIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await dbContext.Products
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price))
            .FirstOrDefaultAsync(cancellationToken);
}