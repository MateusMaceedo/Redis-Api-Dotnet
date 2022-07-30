using Dynamo_Redis_API.Application.Mappers;
using Dynamo_Redis_API.Domain.Entities;
using Dynamo_Redis_API.Domain.Interfaces;
using Dynamo_Redis_API.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace Dynamo_Redis_API.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            var productDto = await _productRepository.GetByIdAsync(productId);
            return productDto?.ToProduct();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var productDto = product.ToProductDto();
            await _productRepository.CreateAsync(productDto);
            return product;
        }

        public async Task<Product> UpdateAsync(Product updatedProduct)
        {
            var productDto = updatedProduct.ToProductDto();
            await _productRepository.UpdateAsync(productDto);
            return updatedProduct;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }
    }
}
