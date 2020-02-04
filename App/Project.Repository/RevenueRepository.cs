using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;

namespace Project.Repository
{
    public interface IRevenueRepository : IBaseRepository<Revenue>
    {
        IQueryable<Revenue> GetRevenuesByDateRange(ReportRequestModel model);
        IQueryable<Revenue> Search(SearchRequestModel request);
    }


    public class RevenueRepository : BaseRepository<Revenue>, IRevenueRepository
    {
        private readonly BusinessDbContext _db;

        public RevenueRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Revenue> GetRevenuesByDateRange(ReportRequestModel model)
        {
            var fromDate = model.ReportFromDate.Date;
            var toDate = model.ReportToDate.Date.AddDays(1).AddSeconds(-1);

            return _db.Revenues.Where(x => x.Created >= fromDate && x.Created <= toDate).AsQueryable();

        }

        public IQueryable<Revenue> Search(SearchRequestModel request)
        {
            if (request.StartDate != new DateTime())
            {
                DateTime startDate = request.StartDate.Date;
                DateTime endDate = startDate.AddDays(1);
                return _db.Revenues.Where(x => x.Created >= startDate && x.Created <= endDate).AsQueryable();
            }

            IQueryable<Revenue> expenses =
                _db.Revenues.Where(x => x.Description.Contains(request.Keyword))
                    .AsQueryable();
            return expenses;
        }
    }
}
