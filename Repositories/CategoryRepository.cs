using MiniStore.Context;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private StoreContext _storeContext;

        public CategoryRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }




        public async Task<Category> AddCategory(Category category)
        {
            await _storeContext.AddAsync(category);
            await _storeContext.SaveChangesAsync();
            return category;
        }




        public  Task<bool> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }




        public IEnumerable<Category> GetAllCategories()
        {
            return _storeContext.Categories.ToList();
        }



        public async Task<Category> GetCatgoryByID(int categoryId)
        {
            return await _storeContext.Categories.FindAsync(categoryId);
        }




        public Task<Category> UpdateCategory(int categoryId, Category category)
        {
            throw new NotImplementedException();
        }


    }
}
