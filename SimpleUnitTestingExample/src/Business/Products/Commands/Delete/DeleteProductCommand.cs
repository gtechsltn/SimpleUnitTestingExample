using Ardalis.Result;
using MediatR;

namespace Business.Products.Commands.Delete;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;