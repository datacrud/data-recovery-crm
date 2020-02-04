using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;

namespace Project.Repository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        double GetTotalPaidAmount(Guid hddInfoId);
        IQueryable<Payment> GetListById(Guid id);
        IQueryable<Payment> GetPaymentByDateRange(ReportRequestModel model);
    }


    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly BusinessDbContext _db;

        public PaymentRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public double GetTotalPaidAmount(Guid hddInfoId)
        {
            return _db.Payments.Where(x => x.HddInfoId == hddInfoId).ToList().Sum(x => x.Amount);
        }

        public IQueryable<Payment> GetListById(Guid id)
        {
            return  _db.Payments.Where(x => x.HddInfoId == id).AsQueryable();
        }

        public IQueryable<Payment> GetPaymentByDateRange(ReportRequestModel model)
        {
            var fromDate = model.ReportFromDate.Date;
            var toDate = model.ReportToDate.Date.AddDays(1).AddSeconds(-1);

            return _db.Payments.Where(x => x.Created >= fromDate && x.Created <= toDate).AsQueryable();
        }
    }
}
