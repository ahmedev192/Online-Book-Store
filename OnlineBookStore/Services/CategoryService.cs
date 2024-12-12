using OnlineBookStore.Database;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStore.Services
{
    public class CategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService()
        {
            _context = DatabaseConnection.Instance.Context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public bool AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            Console.WriteLine($"Category '{category.Name}' added.");
            return true;
        }

        public bool EditCategory(int categoryId, string name)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return false;
            }

            category.Name = name;

            _context.SaveChanges();
            Console.WriteLine($"Category '{name}' updated.");
            return true;
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return false;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            Console.WriteLine($"Category '{category.Name}' deleted.");
            return true;
        }
    }
}
