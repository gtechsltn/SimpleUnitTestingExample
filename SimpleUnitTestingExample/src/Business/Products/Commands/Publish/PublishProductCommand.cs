using Ardalis.Result;
using MediatR;

namespace Business.Products.Commands.Publish;

public sealed record PublishProductCommand(Guid Id) : IRequest<Result>;