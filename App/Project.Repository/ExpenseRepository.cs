using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;

namespace Project.Repository
{
    public interface IExpenseRepository : IBaseRepository<Expense>
    {
        IQueryable<Expense> GetExpensesByDateRange(ReportRequestModel model);
        IQueryable<Expense> Search(SearchRequestModel request);
    }


    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        private readonly BusinessDbContext _db;

        public ExpenseRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Expense> GetExpensesByDateRange(ReportRequestModel model)
        {
            var fromDate = model.ReportFromDate.Date;
            var toDate = model.ReportToDate.Date.AddDays(1).AddSeconds(-1);

            return _db.Expenses.Where(x => x.Created >= fromDate && x.Created <= toDate).AsQueryable();

        }

        public IQueryable<Expense> Search(SearchRequestModel request)
        {
            if (request.StartDate != new DateTime())
            {
                DateTime startDate = request.StartDate.Date;
                DateTime endDate = startDate.AddDays(1);
                return _db.Expenses.Where(x => x.Created >= startDate && x.Created <= endDate).AsQueryable();
            }

            IQueryable<Expense> expenses =
                _db.Expenses.Where(x => x.Description.Contains(request.Keyword))
                    .AsQueryable();
            return expenses;
        }
    }
}
