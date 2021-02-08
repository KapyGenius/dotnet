using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    
    public class FileDishRepository : IDishRepository
    {
        private readonly string _path;

        public FileDishRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "dishes.json");
        }

        public Dish CreateDish(Dish dish)
        {
            var dishes = GetDishes()?.ToList() ?? new List<Dish>();
            if (dishes.Count == 0)
            {
                dish.Id = 0;
            }
            else
            {
                dish.Id = dishes.Max(x => x.Id) + 1;
            }

            dishes.Add(dish);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var Json = JsonSerializer.Serialize(dishes, options);

            File.WriteAllText(_path, Json);

            return dish;


        }

        public void DeleteDish(int Id)
        {
            var dishes = GetDishes().Where(x => x.Id != Id);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var Json = JsonSerializer.Serialize(dishes, options);

            File.WriteAllText(_path, Json);

        }

        public Dish GetDishById(int Id)
        {
            return GetDishes()?.SingleOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Dish> GetDishes()
        {
            var json = File.ReadAllText(_path);

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true

            };

            var dishes = JsonSerializer.Deserialize<Dish[]>(json, option);
            return dishes;
        }

        public Dish UpdateDish(Dish dish)
        {
            var dishes = GetDishes().ToList();
            var dishToUpdate = dishes.SingleOrDefault(x => x.Id == dish.Id);
            dishToUpdate.Name = dish.Name;
            dishToUpdate.Price = dish.Price;
            dishToUpdate.CategoryId = dish.CategoryId;
            dishToUpdate.Description = dish.Description;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var Json = JsonSerializer.Serialize(dishes,options);

            File.WriteAllText(_path, Json);

            return dishToUpdate;
        }
    }
}
