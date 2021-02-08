using System.Collections.Generic;
using NetAngular.Models;

namespace NetAngular.Repositories
{
    public interface IDishRepository
    {
        IEnumerable<Dish> GetDishes();

        Dish GetDishById(int Id);

        Dish CreateDish(Dish dish);

        Dish UpdateDish(Dish dish);

        void DeleteDish(int Id);
    }
}
