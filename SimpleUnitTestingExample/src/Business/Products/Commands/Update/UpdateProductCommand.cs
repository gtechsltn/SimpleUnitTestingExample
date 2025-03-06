using Ardalis.Result;
using MediatR;

namespace Business.Products.Commands.Update;

public sealed record UpdateProductCommand(
    Guid Id,
    string Description,
    decimal Price) : IRequest<Result>;