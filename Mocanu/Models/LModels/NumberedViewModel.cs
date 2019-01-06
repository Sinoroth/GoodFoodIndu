using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Mocanu.Models.LModels
{
    public class NumberedViewModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public int Number { get; set; } = 0;
    }
}