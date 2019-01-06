using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class TransactionViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string ClientId { get; set; }
        public int TotalCost { get; set; }
        public string Address { get; set; }
        public string ID_Card_Series { get; set; }
        public string ID_Card_Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        public DateTime? LoginDate { get; set; }

        public List<NumberedViewModel> Foods { get; set; } = new List<NumberedViewModel>();
    }
}