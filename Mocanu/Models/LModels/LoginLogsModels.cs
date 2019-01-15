using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Mocanu.Models.LModels
{
    public class LoginLogsModels
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        public DateTime? LoginDate { get; set; }
    }
}