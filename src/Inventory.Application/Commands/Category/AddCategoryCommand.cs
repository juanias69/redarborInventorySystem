using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Commands.Category
{
    public record AddCategoryCommand(CategoryDto Product) : IRequest<ResponseResult>;
}
