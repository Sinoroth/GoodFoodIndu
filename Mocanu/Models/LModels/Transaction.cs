using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public List<Food> Foods { get; set; }
        [Display(Name = "Client Id")]
        public string ClientId { get; set; }
        public string Email { get; set; }
        public int TotalCost { get; set; }
        public string Address { get; set; }
        public string ID_Card_Series { get; set; }
        public string ID_Card_Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        [Display(Name = "Purchase Date")]
        public DateTime? Date { get; set; }

        public virtual List<TransactionToFood> transactionToFoods { get; set; } = new List<TransactionToFood>();
    }
}