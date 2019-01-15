using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class ComplexViewModel
    {
        public List<FoodOrderView> foodOrderViews { get; set; } = new List<FoodOrderView>();
    }
}