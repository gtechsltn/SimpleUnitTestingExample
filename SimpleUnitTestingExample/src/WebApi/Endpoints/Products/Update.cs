using Ardalis.Result;
using Business.Products.Commands.Update;
using Mapster;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed record UpdateRequest(string Description, decimal Price);

public sealed class UpdateEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:guid}", async (ISender sender, Guid id, UpdateRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<UpdateProductCommand>() with { Id = id };

            var response = await sender.Send(command, cancellationToken);

            if (response.IsInvalid())
            {
                return Results.BadRequest();
            }

            if (response.IsNotFound())
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags(Tags.Products);
    }
}