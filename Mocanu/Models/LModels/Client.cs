using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }

        public string Email { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(2)]
        [MinLength(2)]
        public string ID_Card_Series { get; set; }

        [MaxLength(6)]
        [MinLength(6)]
        public string ID_Card_Number { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TelephoneNumber { get; set; }

        public string Password { get; set; }
    }
}