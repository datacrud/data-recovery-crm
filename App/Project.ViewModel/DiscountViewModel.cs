using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class DiscountViewModel
    {
        public DiscountViewModel(HddInfo model)
        {
            CaseNo = model.CaseNo;
            DiscountPercent = model.DiscountPercent;
            DiscountAmount = model.DiscountAmount;
            Created = model.Created;
            Modified = model.Modified;
        }

        public string CaseNo { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountAmount { get; set; }
    }
}
