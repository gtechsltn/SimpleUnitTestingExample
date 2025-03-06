using Domain.Entities;
using Domain.Enums;
using Shouldly;

namespace Domain.UnitTests.Entities;

public class ProductTests
{
    [Fact]
    public void Publish_ShouldPublishProduct_WhenProductStatusIsNotPublished()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        // Act
        product.Publish();

        // Assert
        product.Status.ShouldBe(ProductStatus.Published);
    }

    [Fact]
    public void Publish_ShouldThrowException_WhenProductHasAlreadyPublished()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        product.Publish();

        // Act
        var exception = Should.Throw<Exception>(product.Publish);

        // Assert
        exception.Message.ShouldBe($"Product with {product.Id} has already published.");
    }

    [Fact]
    public void Update_ShouldUpdateOrder_Always()
    {
        // Arrange
        var product = new Product(
            Guid.NewGuid(),
            "Name",
            "Description",
            123);

        var newDescription = "New Description";
        var newPrice = 234;

        // Act
        product.Update(newDescription, newPrice);

        // Assert
        product.Description.ShouldBe(newDescription);
        product.Price.ShouldBe(newPrice);
    }
}
