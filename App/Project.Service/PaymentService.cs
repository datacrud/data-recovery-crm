using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface IPaymentService : IBaseService<Payment>
    {
        List<Payment> GetListById(Guid id);
    }

    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly ICaseService _caseService;

        public PaymentService(IPaymentRepository repository, ICaseService caseService): base(repository)
        {
            _repository = repository;
            _caseService = caseService;
        }


        public List<Payment> GetListById(Guid id)
        {
            return  _repository.GetListById(id).ToList();
        }


        public override bool Delete(Guid id)
        {
            var payment = _repository.GetById(id);

            var delete = _repository.Delete(payment);
            var commit = _repository.Commit();

            if (!commit) return false;

            var updatePriceTotal = _caseService.UpdatePriceTotal(payment.HddInfoId);

            return updatePriceTotal;
        }
    }
}
