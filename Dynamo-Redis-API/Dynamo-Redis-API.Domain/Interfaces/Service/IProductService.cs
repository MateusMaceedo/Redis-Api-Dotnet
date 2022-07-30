using Dynamo_Redis_API.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Dynamo_Redis_API.Domain.Interfaces.Service
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(Guid productId);

        Task<Product> CreateAsync(Product product);

        Task<Product> UpdateAsync(Product updatedProduct);

        Task<bool> DeleteAsync(Guid productId);
    }
}
