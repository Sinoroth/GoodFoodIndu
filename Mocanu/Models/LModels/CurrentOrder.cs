using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class CurrentOrder
    {
        [Key]
        public int Id { get; set; }

        public string FoodName { get; set; }
        public int Price { get; set; }
        public int NumberInOrder { get; set; }
    }
}