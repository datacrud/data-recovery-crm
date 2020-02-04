using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class RevenueReportViewModel
    {
        public RevenueReportViewModel()
        {
            
        }
        public RevenueReportViewModel(List<Expense> expenses, List<Payment> payments, double netIncome, double totalExpense, double totalRevenue)
        {
            Expenses = expenses.ConvertAll(x => new ExpenseViewModel(x));
            PaymentRevenues = payments.ConvertAll(x => new RevenuePaymentViewModel(x));
            NetIncome = netIncome;
            TotalExpense = totalExpense;
            TotalRevenue = totalRevenue;
        }

        public List<ExpenseViewModel> Expenses { get; set; }

        public List<RevenuePaymentViewModel> PaymentRevenues { get; set; }

        public List<RevenueReportDataModel> Revenues { get; set; }


    
        public double NetIncome { get; set; }

        public double TotalExpense { get; set; }

        public double TotalRevenue { get; set; }        
    }

    public class RevenueReportDataModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public double Amount { get; set; }

    }
}
