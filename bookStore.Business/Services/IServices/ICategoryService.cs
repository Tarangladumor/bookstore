using bookStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStore.Business.Services.IServices
{
    public interface ICategoryService
    {
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> createCategoryAsync(Category category);
        Task updateCategoryAsync(Category category);
        Task deleteCategoryAsync(int id);

        Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null);

    }
}
