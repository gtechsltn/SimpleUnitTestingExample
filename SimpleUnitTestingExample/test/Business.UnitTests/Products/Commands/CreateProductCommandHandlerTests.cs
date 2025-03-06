using Business.Abstractions;
using Business.Products.Commands.Create;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _applicationDbContext;

    public CreateProductCommandHandlerTests() =>
        _applicationDbContext = new Mock<IApplicationDbContext>();

    [Fact]
    public async Task Handle_ShouldCreateNewProduct_WhenProductNameIsUnique()
    {
        // Arrange
        var command = new CreateProductCommand(
            "Name",
            "Description",
            123);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([]);

        var commandHandler = new CreateProductCommandHandler(_applicationDbContext.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _applicationDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnInvalidResult_WhenProductNameIsNotUnique()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        var command = new CreateProductCommand(
            product.Name,
            "NewDescription",
            234);

        _applicationDbContext.Setup(db => db.Products).ReturnsDbSet([product]);

        var commandHandler = new CreateProductCommandHandler(_applicationDbContext.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.Count().ShouldBe(1);
        result.Errors.First().ShouldBe($"Product with specified name {command.Name} already exists.");
    }
}