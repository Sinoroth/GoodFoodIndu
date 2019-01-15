using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string ClientId { get; set; }
        public int TotalCost { get; set; }
        public string Address { get; set; }
        public string ID_Card_Series { get; set; }
        public string ID_Card_Number { get; set; }

        public virtual List<UserToTransaction> UserToTransactions { get; set; }
    }
}