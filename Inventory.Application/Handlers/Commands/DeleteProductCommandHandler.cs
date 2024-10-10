using Inventory.Application.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseResult>
    {
        private readonly IProductCommandRepository _productRepository;

        public DeleteProductCommandHandler(IProductCommandRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _productRepository.DeleteAsync(request.Id);

                return new ResponseResult() { Success = true, Message = "Registro eliminado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
