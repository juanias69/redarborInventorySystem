using Inventory.Application.Commands.Products;
using Inventory.Application.DTOs;
using Inventory.Application.IServices;
using Inventory.Application.Queries.Products;
using Inventory.Application.Services;
using MediatR;
using Moq;
using Inventory.Domain.Entities;

public class ProductAppServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly IProductAppService _productAppService;

    public ProductAppServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _productAppService = new ProductAppService(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddProductAsync_ShouldSend_AddProductCommand()
    {
        var productDto = new ProductDto { Name = "Tijeras", Price = 2500 };
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AddProductCommand>(), default))
            .ReturnsAsync(expectedResponse);

        var result = await _productAppService.AddProductAsync(productDto);

        Assert.True(result.Success);
        _mediatorMock.Verify(m => m.Send(It.IsAny<AddProductCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldSend_UpdateProductCommand()
    {
        var productDto = new ProductDto { Id = 1, Name = "block cuadriculado", Price = 3700 };
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
            .ReturnsAsync(expectedResponse);

        var result = await _productAppService.UpdateProductAsync(productDto);

        Assert.True(result.Success);
        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateProductCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_ShouldSend_DeleteProductCommand()
    {
        var productId = 1;
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
            .ReturnsAsync(expectedResponse);

        var result = await _productAppService.DeleteProductAsync(productId);

        Assert.True(result.Success);
        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteProductCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldSend_GetProductByIdQuery()
    {
        var productId = 1;
        var expectedProduct = new ProductDto { Id = productId, Name = "resma de papel", Price = 1500 };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
            .ReturnsAsync(expectedProduct);

        var result = await _productAppService.GetProductByIdAsync(productId);

        Assert.Equal(expectedProduct.Id, result.Id);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetProductByIdQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldSend_GetAllProductsQuery()
    {
        var expectedProducts = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Lapiz negro", Price = 10.99M },
            new ProductDto { Id = 2, Name = "Lapiz Rojo", Price = 12.99M }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default))
            .ReturnsAsync(expectedProducts);

        var result = await _productAppService.GetAllProductsAsync();

        Assert.Equal(expectedProducts, result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllProductsQuery>(), default), Times.Once);
    }
}
