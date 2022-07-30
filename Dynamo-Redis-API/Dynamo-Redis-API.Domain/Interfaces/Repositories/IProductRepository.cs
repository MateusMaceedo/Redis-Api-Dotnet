using Dynamo_Redis_API.Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace Dynamo_Redis_API.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto?> GetByIdAsync(Guid productId);

        Task<ProductDto> CreateAsync(ProductDto product);

        Task<ProductDto> UpdateAsync(ProductDto updatedProduct);

        Task<bool> DeleteAsync(Guid productId);
    }
}
