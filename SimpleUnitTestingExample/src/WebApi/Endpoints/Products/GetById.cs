using Business.Products.Queries.GetById;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed class GetByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(id);

            var response = await sender.Send(query, cancellationToken);

            return Results.Ok(response);
        }).WithTags(Tags.Products);
    }
}