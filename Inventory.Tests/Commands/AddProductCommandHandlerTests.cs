using FluentAssertions;
using Inventory.Application.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.Handlers;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Moq;

public class AddProductCommandHandlerTests
{
    private readonly Mock<IProductCommandRepository> _productRepositoryMock;
    private readonly AddProductCommandHandler _handler;

    public AddProductCommandHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductCommandRepository>();
        _handler = new AddProductCommandHandler(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddProduct_WhenCommandIsValid()
    {
        // Arrange
        var productDto = new ProductDto
        {
            Name = "Test Product",
            CategoryId = 1,
            Price = 10.0m
        };

        var command = new AddProductCommand(productDto);

        _productRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()));


        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ResponseResult>(); // Cambia esto según lo que devuelva tu handler
        _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }
}
