using Domain.Enums;

namespace Domain.Entities;

public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }

    public Product(Guid id, string name, string description, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Status = ProductStatus.Created;
    }

    public void Update(string description, decimal price)
    {
        Description = description;
        Price = price;
    }

    public void Publish()
    {
        if (Status == ProductStatus.Published)
        {
            throw new Exception($"Product with {Id} has already published.");
        }

        Status = ProductStatus.Published;
    }
}