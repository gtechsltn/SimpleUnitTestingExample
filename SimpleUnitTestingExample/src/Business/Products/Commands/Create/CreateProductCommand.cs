using Ardalis.Result;
using MediatR;

namespace Business.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price) : IRequest<Result<Guid>>;