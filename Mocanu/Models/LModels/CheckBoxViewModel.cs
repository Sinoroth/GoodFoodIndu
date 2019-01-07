using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class CheckBoxViewModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}