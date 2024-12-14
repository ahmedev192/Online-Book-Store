using OnlineBookStore.Database;
using OnlineBookStore.Models;
using OnlineBookStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService( )
        {
            _categoryRepository = new CategoryRepository();
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        public bool AddCategory(string name)
        {
            var category = new Category { Name = name };
            _categoryRepository.AddCategory(category);
            Console.WriteLine($"Category '{name}' added.");
            return true;
        }

        public bool EditCategory(int categoryId, string name)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return false;
            }

            category.Name = name;
            _categoryRepository.UpdateCategory(category);
            Console.WriteLine($"Category '{name}' updated.");
            return true;
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return false;
            }

            _categoryRepository.DeleteCategory(category);
            Console.WriteLine($"Category '{category.Name}' deleted.");
            return true;
        }
    }
}
