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
    public interface IExpenseService : IBaseService<Expense>
    {
        List<ExpenseViewModel> Search(SearchRequestModel request);
    }

    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository): base(repository)
        {
            _repository = repository;
        }

        public override List<Expense> GetAll()
        {
            return _repository.GetAll().OrderByDescending(x=> x.Modified).Take(10).ToList();
        }

        public List<ExpenseViewModel> Search(SearchRequestModel request)
        {
            List<ExpenseViewModel> expenseViewModels = _repository.Search(request).ToList().ConvertAll(x => new ExpenseViewModel(x));
            return expenseViewModels;
        }
    }
}
