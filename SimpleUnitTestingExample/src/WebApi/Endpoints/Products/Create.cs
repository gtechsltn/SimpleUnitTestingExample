using Business.Products.Commands.Create;
using Mapster;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed record CreateRequest(string Name, string Description, decimal Price);

public sealed class CreateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/", async (ISender sender, CreateRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var response = await sender.Send(command, cancellationToken);

            return response.IsSuccess
                ? Results.Ok(response.Value)
                : Results.BadRequest();
        }).WithTags(Tags.Products);
    }
}