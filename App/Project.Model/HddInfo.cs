 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class HddInfo: Entity
    {
        public Guid CustomerId { get; set; }

        [Index("IX_CaseNo", IsUnique = true)]
        [MaxLength(20)]
        public string CaseNo { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Capacity { get; set; }

        [Required]
        public string InterfaceType { get; set; }

        public string Sl { get; set; }

        [Required]
        public string RequiredData { get; set; }

        public string Note { get; set; }

        public Status Status { get; set; }

        //[Required]
        public string BackupHdd { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReceiveDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate { get; set; }


        public double TotalCost { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountAmount { get; set; }

        public double PaidAmount { get; set; }

        public double DueAmount { get; set; }
        

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public ICollection<CaseLog> CaseLogs { get; set; }
    }
}
