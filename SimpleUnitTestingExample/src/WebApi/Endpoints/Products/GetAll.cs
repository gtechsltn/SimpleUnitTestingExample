using Business.Products.Queries.GetAll;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed class GetAllEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetAllProductsQuery();

            var response = await sender.Send(query, cancellationToken);

            return Results.Ok(response);
        }).WithTags(Tags.Products);
    }
}