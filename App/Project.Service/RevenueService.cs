using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface IRevenueService : IBaseService<Revenue>
    {
        List<RevenueViewModel> Search(SearchRequestModel request);
    }

    public class RevenueService : BaseService<Revenue>, IRevenueService
    {
        private readonly IRevenueRepository _repository;

        public RevenueService(IRevenueRepository repository): base(repository)
        {
            _repository = repository;
        }

        public override List<Revenue> GetAll()
        {
            return _repository.GetAll().OrderByDescending(x=> x.Modified).Take(10).ToList();
        }

        public List<RevenueViewModel> Search(SearchRequestModel request)
        {
            List<RevenueViewModel> expenseViewModels = _repository.Search(request).ToList().ConvertAll(x => new RevenueViewModel(x));
            return expenseViewModels;
        }
    }
}
