using Ardalis.Result;
using Business.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Commands.Publish;

internal sealed class PublishProductCommandHandler(
    IApplicationDbContext dbContext) : IRequestHandler<PublishProductCommand, Result>
{
    public async Task<Result> Handle(PublishProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.NotFound($"Product with specified id {request.Id} is not found.");
        }

        product.Publish();

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}