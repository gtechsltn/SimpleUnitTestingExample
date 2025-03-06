using Business.Abstractions;
using Business.Products.Queries.GetAll;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Queries;

public class GetAllProductsQueryHandlerTests
{

    private readonly Mock<IApplicationDbContext> _applicationDbContext;

    public GetAllProductsQueryHandlerTests() =>
        _applicationDbContext = new Mock<IApplicationDbContext>();

    [Fact]
    public async Task Handle_ShouldReturnAllProducts_Always()
    {
        // Arrange
        var query = new GetAllProductsQuery();

        List<Product> products = [
            new(Guid.NewGuid(), "Name", "Description", 123)
        ];

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet(products);

        var queryHandler = new GetAllProductsQueryHandler(_applicationDbContext.Object);

        // Act
        var result = await queryHandler.Handle(query, default);

        // Assert
        result.Count().ShouldBe(products.Count());
    }
}