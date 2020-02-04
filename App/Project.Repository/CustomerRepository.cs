using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.RequestModel;

namespace Project.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        IQueryable<Customer> Search(SearchRequestModel model);
        IQueryable<Customer> GetLastCreatedCustomer();
    }


    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly BusinessDbContext _db;

        public CustomerRepository(BusinessDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Customer> Search(SearchRequestModel model)
        {
            IQueryable<Customer> customers = null;

            if (string.IsNullOrWhiteSpace(model.Keyword))
            {
                customers = _db.Customers.AsQueryable();
            }

            else
            {
                customers = _db.Customers.Where(
                    x =>
                        x.Code.ToUpper().Contains(model.Keyword.ToUpper()) ||
                        x.Name.ToUpper().Contains(model.Keyword.ToUpper()) ||
                        x.Phone.Contains(model.Keyword) ||
                        x.CompanyName.ToUpper().Contains(model.Keyword) ||
                        x.Email.ToUpper().Contains(model.Keyword.ToUpper())).AsQueryable();
            }   

            return customers;
        }

        public IQueryable<Customer> GetLastCreatedCustomer()
        {
            var queryable = _db.Customers.OrderByDescending(x => x.Created).Take(1);

            return queryable;
        }
    }
}
