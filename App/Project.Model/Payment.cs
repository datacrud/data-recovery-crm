using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Payment : Entity
    {
        public Guid HddInfoId { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime PaymentDate { get; set; }

        public double Amount { get; set; }

        public string FeesType { get; set; }

        [ForeignKey("HddInfoId")]
        public virtual HddInfo HddInfo { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
