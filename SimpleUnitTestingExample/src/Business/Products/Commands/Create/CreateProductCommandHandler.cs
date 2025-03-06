using Ardalis.Result;
using Business.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Products.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (exists)
        {
            return Result.Error($"Product with specified name {request.Name} already exists.");
        }

        var product = new Product(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Price);

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);
    }
}