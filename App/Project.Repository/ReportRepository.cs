using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Repository
{
    public interface IReportRepository
    {
        
    }

    public class ReportRepository: IReportRepository
    {
        private readonly BusinessDbContext _db;
        
        public ReportRepository(BusinessDbContext db)
        {
            _db = db;
        }

        
    }
}
