using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class FoodOrderView
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string ImageLink { get; set; }
        public string FoodIngredients { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string FoodType { get; set; }
        public int NumberInOrder { get; set; }

        public virtual IEnumerable<FoodtoFoodIngredients> FoodtoFoodIngredients { get; set; } = new List<FoodtoFoodIngredients>();
    }
}