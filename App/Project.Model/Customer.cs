using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Customer: Entity
    {
        //[Index("IX_Code", IsUnique = true)]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }        

        public string CompanyName { get; set; }

        public string Reference { get; set; }

        public virtual ICollection<HddInfo> HddInfos { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
