using Inventory.Application.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.IServices;
using Inventory.Application.Queries;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMediator mediator;

        public ProductAppService(IMediator mediator)
        {
            mediator = mediator;
        }

        public async Task<ResponseResult> AddProductAsync(ProductDto productDto)
        {
            var command = new AddProductCommand(productDto);
            return await mediator.Send(command);
        }

        public async Task<ResponseResult> UpdateProductAsync(ProductDto productDto)
        {
            var command = new UpdateProductCommand(productDto);
            return await mediator.Send(command);
        }

        public async Task<ResponseResult> DeleteProductAsync(int id)
        {
            var command = new DeleteProductCommand(id);
            return await mediator.Send(command);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            ProductDto product = new();
            var query = new GetProductByIdQuery(id);
            var productResult = await mediator.Send(query);
            if(productResult != null)
            {
                product.Id = productResult.Id;
                product.Name = productResult.Name;  
                product.Description = productResult.Description;
                product.Price  = productResult.Price;
                product.CategoryId = productResult.CategoryId;
            }
                 
             return product;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var query = new GetAllProductsQuery();
            return (IEnumerable<ProductDto>)await mediator.Send(query);
        }
    }

}
