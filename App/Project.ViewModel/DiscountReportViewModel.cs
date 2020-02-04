using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class DiscountReportViewModel
    {        


        public DiscountReportViewModel(List<HddInfo> cases, double totalDiscountPercent, double totalDiscountAmount)
        {
            Discounts = cases.ConvertAll(x => new DiscountViewModel(x));
            TotalDiscountPercent = totalDiscountPercent;
            TotalDiscountAmount = totalDiscountAmount;
        }

        public List<DiscountViewModel> Discounts { get; set; }

        public double TotalDiscountPercent { get; set; }

        public double TotalDiscountAmount { get; set; }
    }
}
