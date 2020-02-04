using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface ICaseLogService : IBaseService<CaseLog>
    {
        List<CaseLogViewModel> GetCaseLog(Guid id);
    }

    public class CaseLogService : BaseService<CaseLog>, ICaseLogService
    {
        private readonly ICaseLogRepository _repository;

        public CaseLogService(ICaseLogRepository repository): base(repository)
        {
            _repository = repository;
        }

        public List<CaseLogViewModel> GetCaseLog(Guid id)
        {
            var list = _repository.GetCaseLog(id).ToList().ConvertAll(x=> new CaseLogViewModel(x));
            return list;
        }
    }
}
