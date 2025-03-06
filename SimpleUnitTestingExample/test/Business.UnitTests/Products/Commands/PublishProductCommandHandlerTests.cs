using Business.Abstractions;
using Business.Products.Commands.Publish;
using Domain.Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Shouldly;

namespace Business.UnitTests.Products.Commands;

public class PublishProductCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _applicationDbContextMock;

    public PublishProductCommandHandlerTests() =>
        _applicationDbContextMock = new Mock<IApplicationDbContext>();

    [Fact]
    public async Task Handle_ShouldPublishProduct_WhenProductWasNotPublished()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        var command = new PublishProductCommand(product.Id);

        _applicationDbContextMock.Setup(db => db.Products).ReturnsDbSet([product]);

        var commandHandler = new PublishProductCommandHandler(_applicationDbContextMock.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        _applicationDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowAnException_WhenProductWasAlreadyPublished()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        product.Publish();

        var command = new PublishProductCommand(product.Id);

        _applicationDbContextMock.Setup(db => db.Products).ReturnsDbSet([product]);

        var commandHandler = new PublishProductCommandHandler(_applicationDbContextMock.Object);

        // Act
        var exception = await Should.ThrowAsync<Exception>(commandHandler.Handle(command, default));

        // Assert
        exception.Message.ShouldBe($"Product with {product.Id} has already published.");
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenProductIsNotFound()
    {
        // Arrange
        var command = new PublishProductCommand(Guid.NewGuid());

        _applicationDbContextMock.Setup(db => db.Products).ReturnsDbSet([]);

        var commandHandler = new PublishProductCommandHandler(_applicationDbContextMock.Object);

        // Act
        var result = await commandHandler.Handle(command, default);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.Count().ShouldBe(1);
        result.Errors.First().ShouldBe($"Product with specified id {command.Id} is not found.");
    }
}