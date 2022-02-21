using MiniStore.Models;
using MiniStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public class ProductService : IProductService
    {


        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Product> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProduct(productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductByID(int productId)
        {
            return await _productRepository.GetProductByID(productId);
        }

        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            return await _productRepository.UpdateProduct(productId, product);
        }
    }
}
