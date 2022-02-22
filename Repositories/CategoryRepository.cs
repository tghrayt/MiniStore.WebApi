using Microsoft.EntityFrameworkCore;
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
        private readonly StoreContext _storeContext;

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




        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _storeContext.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new ArgumentException("Cette categorie n'exsite plus !");
            }

            var categeryDeleted = _storeContext.Categories.Remove(category);
            await _storeContext.SaveChangesAsync();

            if (categeryDeleted == null)
            {
                return false;
            }
            return true;
        }




        public IEnumerable<Category> GetAllCategories()
        {
            return _storeContext.Categories.ToList();
        }



        public async Task<Category> GetCatgoryByID(int categoryId)
        {
            return await _storeContext.Categories.FindAsync(categoryId);
        }




        public async Task<Category> UpdateCategory(int categoryId, Category category)
        {
            var categoryToUpdate = await _storeContext.Categories.FirstAsync(a => a.CategoryId== categoryId);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException("Cette categorie n'exsite pas !");
            }

            categoryToUpdate.CategoryName = category.CategoryName;
            await _storeContext.SaveChangesAsync();

            return categoryToUpdate;
        }


    }
}
