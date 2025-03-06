using Business.Products.Queries.GetById;
using MediatR;

namespace Business.Products.Queries.GetAll;

public sealed record GetAllProductsQuery : IRequest<IEnumerable<ProductResponse?>>;