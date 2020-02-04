using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.RequestModel;

namespace Project.Service
{
    public interface ICustomerService : IBaseService<Customer>
    {
        List<Customer> Search(SearchRequestModel model);
        string GetLastCreatedCustomerCode();
    }

    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository): base(repository)
        {
            _repository = repository;
        }

        public List<Customer> Search(SearchRequestModel model)
        {
            return  _repository.Search(model).ToList();
        }

        public string GetLastCreatedCustomerCode()
        {
            var customers = _repository.GetLastCreatedCustomer().ToList();

            if (customers.Count == 0) return "00100";

            return customers.Count != 1 ? null : customers.Select(customer => customer.Code).FirstOrDefault();
        }
    }
}
