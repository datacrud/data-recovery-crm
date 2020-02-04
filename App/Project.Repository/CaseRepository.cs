using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;

namespace Project.Repository
{
    public interface ICaseRepository : IBaseRepository<HddInfo>
    {
        IQueryable<HddInfo> Search(SearchRequestModel request);
        IQueryable<HddInfo> LastCreatedCase();
        IQueryable<HddInfo> GetCasesByDateRange(ReportRequestModel model);
        IQueryable<HddInfo> SearchPrintableCase(string keyword);
    }


    public class CaseRepository : BaseRepository<HddInfo>, ICaseRepository
    {
        private readonly BusinessDbContext _db;

        public CaseRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<HddInfo> Search(SearchRequestModel request)
        {
            IQueryable<HddInfo> hddInfos = null;

            if (request.Filter == Status.All && string.IsNullOrWhiteSpace(request.Keyword))
                hddInfos = _db.HddInfos.AsQueryable();

            if (request.Filter == Status.All && !string.IsNullOrWhiteSpace(request.Keyword))
                hddInfos =
                    _db.HddInfos.Where(
                        x =>
                            x.CaseNo.ToUpper().Contains(request.Keyword) ||
                            x.Customer.Name.ToUpper().Contains(request.Keyword.ToUpper()) ||
                            x.Sl.ToLower().Contains(request.Keyword.ToLower())).AsQueryable();


            if (request.Filter != Status.All && string.IsNullOrWhiteSpace(request.Keyword))
            {
                hddInfos = _db.HddInfos.Where(x => x.Status == request.Filter).AsQueryable();
            }

            if (request.Filter != Status.All && !string.IsNullOrWhiteSpace(request.Keyword))
            {
                hddInfos =
                    _db.HddInfos.Where(
                        x =>
                            x.Status == request.Filter && (x.CaseNo.ToUpper().Contains(request.Keyword)) ||
                            x.Customer.Name.ToUpper().Contains(request.Keyword.ToUpper()) ||
                            x.Sl.ToLower().Contains(request.Keyword.ToLower())).AsQueryable();
            }

            return hddInfos;
        }

        public IQueryable<HddInfo> LastCreatedCase()
        {
            var queryable = _db.HddInfos.OrderByDescending(x => x.Created).Take(1).AsQueryable();

            return queryable;
        }

        public IQueryable<HddInfo> GetCasesByDateRange(ReportRequestModel model)
        {
            var stateDate = model.ReportFromDate.Date;
            var endDate = model.ReportToDate.Date.AddDays(1).AddSeconds(-1);

            var queryable = _db.HddInfos.Where(x => x.Created >= stateDate && x.Created <= endDate).AsQueryable();

            return queryable;
        }

        public IQueryable<HddInfo> SearchPrintableCase(string keyword)
        {
            return _db.HddInfos.Where(x => x.CaseNo.Contains(keyword)).AsQueryable();
        }
    }
}
