using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        bool AddCategory(string name);
        bool EditCategory(int categoryId, string name);
        bool DeleteCategory(int categoryId);
    }
}
