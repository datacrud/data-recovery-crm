using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class ExpenseReportViewModel
    {

        public ExpenseReportViewModel(List<Expense> expenses, double totalExpense)
        {
            Expenses = expenses.ConvertAll(x => new ExpenseViewModel(x));
            TotalExpense = totalExpense;
        }     

        public List<ExpenseViewModel> Expenses { get; set; }

        public double TotalExpense { get; set; }
    }
}
