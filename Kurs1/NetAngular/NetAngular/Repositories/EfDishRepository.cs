using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    public class EfDishRepository:IDishRepository
    {
        private readonly VegiContext _vegiContext;

        public EfDishRepository(VegiContext vegiContext)
        {
            _vegiContext = vegiContext;
        }

        public Dish CreateDish(Dish dish)
        {
            _vegiContext.Dishes.Add(dish);
            _vegiContext.SaveChanges();
            return dish;
        }

        public void DeleteDish(int Id)
        {
            var dish = _vegiContext.Dishes.Find(Id);
            _vegiContext.Dishes.Remove(dish);
            _vegiContext.SaveChanges();
        }

        public Dish GetDishById(int Id)
        {
            var dish = _vegiContext.Dishes.Find(Id);
            return dish;
        }

        public IEnumerable<Dish> GetDishes()
        {
            var dishes = _vegiContext.Dishes
                                            .AsNoTracking()
                                            .ToList();
            return dishes;
        }

        public Dish UpdateDish(Dish dish)
        {
            var dishToUpdate = _vegiContext.Dishes.Find(dish.Id);

            dishToUpdate.Name = dish.Name;
            dishToUpdate.Description = dish.Description;
            dishToUpdate.Price = dish.Price;

            _vegiContext.SaveChanges();

            return dishToUpdate;
        }
    }
}
