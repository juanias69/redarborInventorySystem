using Inventory.Application.Commands.Products;
using Inventory.Application.DTOs;
using Inventory.Application.IServices;
using Inventory.Application.Queries.Products;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMediator _mediator;

        public ProductAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseResult> AddProductAsync(ProductDto productDto)
        {
            var command = new AddProductCommand(productDto);
            return await _mediator.Send(command);
        }

        public async Task<ResponseResult> UpdateProductAsync(ProductDto productDto)
        {
            var command = new UpdateProductCommand(productDto);
            return await _mediator.Send(command);
        }

        public async Task<ResponseResult> DeleteProductAsync(int id)
        {
            var command = new DeleteProductCommand(id);
            return await _mediator.Send(command);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var query = new GetProductByIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var query = new GetAllProductsQuery();
            return await _mediator.Send(query);
        }
    }

}
