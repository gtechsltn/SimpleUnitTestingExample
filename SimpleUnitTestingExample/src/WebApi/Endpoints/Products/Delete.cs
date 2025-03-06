using Business.Products.Commands.Delete;
using MediatR;
using WebApi.Abstractions;

namespace WebApi.Endpoints.Products;

public sealed class DeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var command = new DeleteProductCommand(id);

            var response = await sender.Send(command, cancellationToken);

            return response.IsSuccess
                ? Results.NoContent()
                : Results.NotFound();
        }).WithTags(Tags.Products);
    }
}