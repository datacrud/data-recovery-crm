using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public class Revenue : Entity
    {
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}