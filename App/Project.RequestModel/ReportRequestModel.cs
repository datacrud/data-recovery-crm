using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RequestModel
{
    public class ReportRequestModel
    {
        public DateTime ReportFromDate { get; set; }

        public DateTime ReportToDate { get; set; }

        public ReportType ReportType { get; set; }
    }

    public enum ReportType
    {
        Expense,
        Revenue,
        Discount,
        Customer
    }
}
