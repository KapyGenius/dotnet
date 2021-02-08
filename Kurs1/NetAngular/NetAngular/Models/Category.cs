using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetAngular.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new HashSet<Dish>();
    }
}