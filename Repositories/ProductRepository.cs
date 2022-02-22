using Microsoft.EntityFrameworkCore;
using MiniStore.Context;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;


        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _storeContext.Products.AddAsync(product);
            await _storeContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _storeContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException("Ce produit n'exsite plus !");
            }
            var productDeleted = _storeContext.Products.Remove(product);
            await _storeContext.SaveChangesAsync();

            if (productDeleted == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _storeContext.Products.ToList();
        }

        public async Task<Product> GetProductByID(int productId)
        {
            return await _storeContext.Products.FindAsync(productId);
        }

        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            var productToUpdate = await _storeContext.Products.FirstAsync(a=>a.ProductId == productId);
            if (productToUpdate == null)
            {
                throw new ArgumentException("Ce produit n'exsite pas !");
            }

            productToUpdate.ProductDescription = product.ProductDescription;
            productToUpdate.Productmanufacturing = product.Productmanufacturing;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Category_Prodcut_Id = product.Category_Prodcut_Id;
            productToUpdate.category = product.category;
            await _storeContext.SaveChangesAsync();

            return productToUpdate;
        }
    }
}
