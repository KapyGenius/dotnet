using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetAngular.Repositories;

namespace NetAngular.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var vegiContext = new VegiContext(serviceProvider.GetRequiredService<DbContextOptions<VegiContext>>()))
            {
                if (vegiContext.Dishes.Any())
                {
                    return;
                }
                var categoryRepository = new FileCategoryRepository(serviceProvider.GetRequiredService<IWebHostEnvironment>());

                var categories = categoryRepository.GetCategories().ToList();

                categories.ForEach(c =>
                {
                    c.Id = 0;
                    foreach (var dish in c.Dishes)
                    {
                        dish.Id = 0;
                        dish.CategoryId = 0;

                    }
                });

                vegiContext.Categories.AddRange(categories);
                vegiContext.SaveChanges();


            }

        }
    }
}
