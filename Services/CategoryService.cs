using MiniStore.Models;
using MiniStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService( ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }




        public async Task<Category> AddCategory(Category category)
        {
            return await _categoryRepository.AddCategory(category);
        }




        public async Task<bool> DeleteCategory(int categoryId)
        {
           return await _categoryRepository.DeleteCategory(categoryId);
        }




        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }



        public async Task<Category> GetCatgoryByID(int categoryId)
        {
           return await _categoryRepository.GetCatgoryByID(categoryId);
    }




        public async Task<Category> UpdateCategory(int categoryId, Category category)
        {
            return await _categoryRepository.UpdateCategory(categoryId, category);
        }
    }
}
