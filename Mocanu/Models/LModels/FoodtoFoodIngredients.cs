using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocanu.Models.LModels
{
    public class FoodtoFoodIngredients
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string IngredientId { get; set; }

        public virtual Food food { get; set; }
        public virtual FoodIngredient Ingredient { get; set; }
    }
}