using Ardalis.Result;
using Business.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Commands.Update;

internal sealed class UpdateProductCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.NotFound($"Product with specified id {request.Id} is not found.");
        }

        product.Update(request.Description, request.Price);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}