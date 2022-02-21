using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
   public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Task<Product> GetProductByID(int productId);
        public Task<bool> DeleteProduct(int productId);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(int productId, Product product);
    }
}
