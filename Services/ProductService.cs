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
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<Product> AddProduct(Product product)
        {
            var category = _categoryRepository.GetCatgoryByID(product.Category_Prodcut_Id);
            if (category == null)
            {
                throw new ArgumentException("Veillez choisir une catégorie valide !");
            }
            product.category = category.Result;
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

            var category = _categoryRepository.GetCatgoryByID(product.Category_Prodcut_Id);
            if (category == null)
            {
                throw new ArgumentException("Veillez choisir une catégorie valide !");
            }
            product.category = category.Result;
            return await _productRepository.UpdateProduct(productId, product);
        }
    }
}
