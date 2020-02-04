using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class CaseLog: Entity
    {
        public Guid HddInfoId { get; set; }

        public Status Status { get; set; }

        [ForeignKey("HddInfoId")]
        public virtual HddInfo HddInfo { get; set; }
    }
}
