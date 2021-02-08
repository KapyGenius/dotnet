using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    public class FileCategoryRepository : ICategorieRepository
    {
        private readonly string _catpath;
        private readonly string _dishpath;

        public FileCategoryRepository(IWebHostEnvironment env)
        {
            _catpath = Path.Combine(env.ContentRootPath, "data", "categories.json");
            _dishpath = Path.Combine(env.ContentRootPath, "data", "dishes.json");
        }

        public Category CreateCategory(Category category)
        {
            var categories = GetCategories().ToList();
            if(categories.Count == 0)
            {
                category.Id = 1;
            }
            else
            {
                category.Id = categories.Max(x => x.Id) + 1;
            }

            categories.Add(category);

            var option = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(categories, option);

            File.WriteAllText(_catpath, json);

            return category;

        }

        public void DeleteCategorie(int Id)
        {
            var categories = GetCategories().Where(x => x.Id != Id);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var Json = JsonSerializer.Serialize(categories, options);

            File.WriteAllText(_catpath, Json);

        }

        public Category GetCategoryById(int catId)
        {
            var category = GetCategories()?.SingleOrDefault(x => x.Id == catId);
            return category;
        }

        public IEnumerable<Category> GetCategories()
        {
            var Json = File.ReadAllText(_catpath);
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            var cat = JsonSerializer.Deserialize<Category[]>(Json, options);

            var json = File.ReadAllText(_dishpath);

            var dishes = JsonSerializer.Deserialize<Dish[]>(json, options);

            foreach (var category in cat)
            {
                var catDishes = dishes.Where(x => x.CategoryId == category.Id).ToList();
                category.Dishes = catDishes;
            }

            return cat;
            
        }

        public Category UpdateCategory(Category category)
        {
            var categories = GetCategories();
            var categoryToUpdate = categories.SingleOrDefault(x => x.Id == category.Id);
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(categories, options);

            File.WriteAllText(_catpath, json);

            return categoryToUpdate;
        }
    }
}
