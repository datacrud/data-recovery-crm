using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Expense: Entity
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
