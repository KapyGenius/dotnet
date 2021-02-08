using System.Collections.Generic;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    public interface ICategorieRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int Id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategorie(int Id);
    }
}
