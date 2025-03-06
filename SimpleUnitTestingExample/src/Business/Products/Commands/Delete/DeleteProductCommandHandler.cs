using Ardalis.Result;
using Business.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Commands.Delete;

internal sealed class DeleteProductCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.NotFound($"Product with specified id {request.Id} is not found.");
        }

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}