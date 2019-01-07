using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class FoodViewsModel
    {
        [Key]
        public string FoodName { get; set; }
        public int Price { get; set; }

        public string ImageLink { get; set; }
        public int Quantity { get; set; }
        public string FoodType { get; set; }

        public List<CheckBoxViewModel> FoodIngredients { get; set; } = new List<CheckBoxViewModel>();
    }
}