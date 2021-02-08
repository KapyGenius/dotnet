using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    public class EfCategoryRepository:ICategorieRepository
    {
        private readonly VegiContext _vegiContext;

        public EfCategoryRepository(VegiContext vegiContext)
        {
            _vegiContext = vegiContext;
        }

        public Category CreateCategory(Category category)
        {
            _vegiContext.Categories.Add(category);
            _vegiContext.SaveChanges();
            return category;
        }

        public void DeleteCategorie(int Id)
        {
            var category = _vegiContext.Categories.Find(Id);
            _vegiContext.Categories.Remove(category);
            _vegiContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _vegiContext.Categories
                                                .AsNoTracking()
                                                .Include(x => x.Dishes)
                                                .ToList();
            return categories;
        }

        public Category GetCategoryById(int Id)
        {
            var category = _vegiContext.Categories.Find(Id);
            _vegiContext.Entry(category).Collection(x => x.Dishes).Load();
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            var CategoryToUpdate = _vegiContext.Categories.Find(category.Id);

            CategoryToUpdate.Name = category.Name;
            CategoryToUpdate.Description = category.Description;

            _vegiContext.SaveChanges();

            return CategoryToUpdate;

        }
    }
}
