using Business.Abstractions;
using Business.Products.Commands.Update;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Commands;

public class UpdateProductCommandHandlerTests
{

    private readonly Mock<IApplicationDbContext> _applicationDbContext;

    public UpdateProductCommandHandlerTests() =>
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

        var command = new UpdateProductCommand(product.Id, "NewDescription", 234);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([product]);

        var commandHandler = new UpdateProductCommandHandler(_applicationDbContext.Object);

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
        var command = new UpdateProductCommand(Guid.NewGuid(), "NewDescription", 234);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([]);

        var commandHandler = new UpdateProductCommandHandler(_applicationDbContext.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.Count().ShouldBe(1);
        result.Errors.First().ShouldBe($"Product with specified id {command.Id} is not found.");
    }
}