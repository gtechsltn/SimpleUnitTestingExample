using Business.Abstractions;
using Business.Products.Queries.GetById;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Queries;

public class GetProductByIdQueryHandlerTests
{

    private readonly Mock<IApplicationDbContext> _applicationDbContext;

    public GetProductByIdQueryHandlerTests() =>
        _applicationDbContext = new Mock<IApplicationDbContext>();

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenProductIsNotFound()
    {
        // Arrange
        var query = new GetProductByIdQuery(Guid.NewGuid());

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([]);

        var queryHandler = new GetProductByIdQueryHandler(_applicationDbContext.Object);

        // Act
        var result = await queryHandler.Handle(query, default);

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductResponse_WhenProductIsFound()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        var query = new GetProductByIdQuery(product.Id);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([product]);

        var queryHandler = new GetProductByIdQueryHandler(_applicationDbContext.Object);

        // Act
        var result = await queryHandler.Handle(query, default);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(product.Id);
        result.Name.ShouldBe(product.Name);
        result.Description.ShouldBe(product.Description);
        result.Price.ShouldBe(product.Price);
    }
}