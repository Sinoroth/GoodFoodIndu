using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class Food
    {
        [Key]
        public string FoodName { get; set; }
        public string ImageLink { get; set; }
        public string FoodIngredients { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string FoodType { get; set; }
    }
}