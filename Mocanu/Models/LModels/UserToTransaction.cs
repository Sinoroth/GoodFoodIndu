using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class UserToTransaction
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int TransactionId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}