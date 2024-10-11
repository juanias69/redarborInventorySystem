﻿using FluentAssertions;
using Inventory.Application.Commands.Inventory;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Queries.Inventories;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class InventoryAppServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly IInventoryAppService _inventoryAppService;

    public InventoryAppServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _inventoryAppService = new InventoryAppService(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddInventoryAsync_ShouldReturnResponseResult_WhenCommandIsExecuted()
    {
        // Arrange
        var inventoryDto = new InventoryDto { ProductId = 1, Quantity = 100 };
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AddInventoryCommand>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _inventoryAppService.AddInventoryAsync(inventoryDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<AddInventoryCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task UpdateInventoryAsync_ShouldReturnResponseResult_WhenCommandIsExecuted()
    {
        // Arrange
        var inventoryDto = new InventoryDto { ProductId = 1, Quantity = 150 };
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateInventoryCommand>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _inventoryAppService.UpdateInventoryAsync(inventoryDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<UpdateInventoryCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GetInventoryByProductIdAsync_ShouldReturnInventoryDto_WhenQueryIsExecuted()
    {
        // Arrange
        var productId = 1;
        var expectedInventoryDto = new InventoryDto { ProductId = productId, Quantity = 100 };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetInventarytByProductIdQuery>(), default))
            .ReturnsAsync(expectedInventoryDto);

        // Act
        var result = await _inventoryAppService.GetInventoryByProductIdAsync(productId);

        // Assert
        result.Should().BeEquivalentTo(expectedInventoryDto);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetInventarytByProductIdQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task GetAllInventoriesAsync_ShouldReturnInventoryDtoList_WhenQueryIsExecuted()
    {
        // Arrange
        var expectedInventories = new List<InventoryDto>
        {
            new InventoryDto { ProductId = 1, Quantity = 100 },
            new InventoryDto { ProductId = 2, Quantity = 150 }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllInventoriesQuery>(), default))
            .ReturnsAsync(expectedInventories);

        // Act
        var result = await _inventoryAppService.GetAllInventoriesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedInventories);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllInventoriesQuery>(), default), Times.Once);
    }
}
