using MediatR;

namespace Business.Products.Queries.GetById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;