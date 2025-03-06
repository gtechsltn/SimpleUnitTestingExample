namespace Business.Products.Queries.GetById;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price);