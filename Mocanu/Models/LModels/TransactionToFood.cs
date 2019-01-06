using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class TransactionToFood
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int TransactionId { get; set; }
        public int Number { get; set; }

        public virtual Food Food { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}