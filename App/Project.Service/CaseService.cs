using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface ICaseService : IBaseService<HddInfo>
    {
        bool UpdatePriceTotal(Guid hddInfoId);
        List<CaseViewModel> Search(SearchRequestModel request);
        string LastCreatedCaseCode();
        List<CaseViewModel> SearchPrintableCase(string keyword);
    }

    public class CaseService : BaseService<HddInfo>, ICaseService
    {
        private readonly ICaseRepository _repository;
        private readonly IPaymentRepository _paymentRepository;

        public CaseService(ICaseRepository repository, IPaymentRepository paymentRepository): base(repository)
        {
            _repository = repository;
            _paymentRepository = paymentRepository;
        }



        public bool UpdatePriceTotal(Guid hddInfoId)
        {
            var caseInfo = _repository.GetById(hddInfoId);
            caseInfo.PaidAmount = _paymentRepository.GetTotalPaidAmount(hddInfoId);
            caseInfo.DueAmount = caseInfo.TotalCost - (caseInfo.PaidAmount + caseInfo.DiscountAmount);

            try
            {
                _repository.Edit(caseInfo);
                _repository.Commit();
            }
            catch (Exception exception)
            {
               throw new Exception(exception.Message);
            }
            return true;
        }

        public List<CaseViewModel> Search(SearchRequestModel request)
        {
            var viewModels = _repository.Search(request).ToList().ConvertAll(x=> new CaseViewModel(x));

            return viewModels;
        }

        public string LastCreatedCaseCode()
        {
            var cases = _repository.LastCreatedCase().ToList();

            if (cases.Count == 0) return "03000";

            return cases.Count != 1 ? null : cases.Select(c => c.CaseNo).FirstOrDefault();
        }

        public List<CaseViewModel> SearchPrintableCase(string keyword)
        {
            List<CaseViewModel> caseViewModels = _repository.SearchPrintableCase(keyword).ToList().ConvertAll(x=> new CaseViewModel(x));

            return caseViewModels;
        }
    }
}
