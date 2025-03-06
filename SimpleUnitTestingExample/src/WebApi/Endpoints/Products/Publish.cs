using Ardalis.Result;
using Business.Products.Commands.Publish;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed class PublishEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:guid}/publish", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var command = new PublishProductCommand(id);

            var response = await sender.Send(command, cancellationToken);

            if (response.IsNotFound())
            {
                return Results.NotFound();
            }

            if (response.IsError())
            {
                return Results.BadRequest();
            }

            return Results.NoContent();
        }).WithTags(Tags.Products);
    }
}