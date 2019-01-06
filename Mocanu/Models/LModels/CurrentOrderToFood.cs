using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Mocanu.Models.LModels
{
    public class CurrentOrderToFood
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int TransactionId { get; set; }
        public int Number { get; set; }

        public virtual Food Food { get; set; }
        public virtual CurrentOrder Order { get; set; }
    }
}