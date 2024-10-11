using FluentAssertions;
using Inventory.Application.Commands.Category;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Queries.Categories;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class CategoryAppServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ICategoryAppService _categoryAppService;

    public CategoryAppServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _categoryAppService = new CategoryAppService(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddCategoryAsync_ShouldReturnResponseResult_WhenCommandIsExecuted()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Papeleria" };
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AddCategoryCommand>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _categoryAppService.AddCategoryAsync(categoryDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<AddCategoryCommand>(), default), Times.Once);
    }

    [Fact]
    public async Task GetAllCategoriesAsync_ShouldReturnCategoryDtoList_WhenQueryIsExecuted()
    {
        // Arrange
        var expectedCategories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Papeleria" },
            new CategoryDto { Id = 2, Name = "Vestuario" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllCategoriesQuery>(), default))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _categoryAppService.GetAllCategoriesAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedCategories);
        _mediatorMock.Verify(m => m.Send(It.IsAny<GetAllCategoriesQuery>(), default), Times.Once);
    }

    [Fact]
    public async Task DeleteCategoryAsync_ShouldReturnResponseResult_WhenCommandIsExecuted()
    {
        // Arrange
        var categoryId = 1;
        var expectedResponse = new ResponseResult { Success = true };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _categoryAppService.DeleteCategoryAsync(categoryId);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        _mediatorMock.Verify(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default), Times.Once);
    }
}
