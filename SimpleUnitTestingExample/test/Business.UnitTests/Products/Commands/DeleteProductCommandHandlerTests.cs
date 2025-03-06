using Business.Abstractions;
using Business.Products.Commands.Delete;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Commands;

public class DeleteProductCommandHandlerTests
{

    private readonly Mock<IApplicationDbContext> _applicationDbContext;

    public DeleteProductCommandHandlerTests() =>
        _applicationDbContext = new Mock<IApplicationDbContext>();

    [Fact]
    public async Task Handle_ShouldUpdateProduct_WhenProductIsFound()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        var command = new DeleteProductCommand(product.Id);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([product]);

        var commandHandler = new DeleteProductCommandHandler(_applicationDbContext.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _applicationDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenProductIsNotFound()
    {
        // Arrange
        var command = new DeleteProductCommand(Guid.NewGuid());

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([]);

        var commandHandler = new DeleteProductCommandHandler(_applicationDbContext.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.Count().ShouldBe(1);
        result.Errors.First().ShouldBe($"Product with specified id {command.Id} is not found.");
    }
}