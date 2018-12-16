using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class FoodIngredient
    {
        [Key]
        public string FoodIngredientId { get; set; }
        public string Ingredient { get; set; }

        public virtual IEnumerable<FoodtoFoodIngredients> FoodtoFoodIngredients { get; set; }
    }
}