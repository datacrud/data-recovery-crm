using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Repository
{
    public interface ICaseLogRepository : IBaseRepository<CaseLog>
    {
        IQueryable<CaseLog> GetCaseLog(Guid id);
    }


    public class CaseLogRepository : BaseRepository<CaseLog>, ICaseLogRepository
    {
        private readonly BusinessDbContext _db;

        public CaseLogRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<CaseLog> GetCaseLog(Guid id)
        {
            return  _db.CaseLogs.Where(x => x.HddInfoId == id).AsQueryable();
        }
    }
}
