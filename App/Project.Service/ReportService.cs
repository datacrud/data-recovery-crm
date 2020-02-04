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
    public interface IReportService
    {
        ExpenseReportViewModel GetExpenseReport(ReportRequestModel model);
        RevenueReportViewModel GetRevenueReport(ReportRequestModel model);
        DiscountReportViewModel GetDiscountReport(ReportRequestModel model);
        CustomerReportViewModel GetCustomerReport(ReportRequestModel model);
    }

    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IPaymentRepository _payment;
        private readonly IExpenseRepository _expense;
        private readonly ICaseRepository _case;
        private readonly IRevenueRepository _revenueRepository;


        public ReportService(IReportRepository repository, IPaymentRepository payment, IExpenseRepository expense, ICaseRepository caseRepository, IRevenueRepository revenueRepository)
        {
            _repository = repository;
            _payment = payment;
            _expense = expense;
            _case = caseRepository;
            _revenueRepository = revenueRepository;
        }

        public ExpenseReportViewModel GetExpenseReport(ReportRequestModel model)
        {
            var expenses = _expense.GetExpensesByDateRange(model).ToList();

            var totalExpense = expenses.Sum(x => x.Amount);

            var viewModels = new ExpenseReportViewModel(expenses, totalExpense);

            return viewModels;
        }

        public RevenueReportViewModel GetRevenueReport(ReportRequestModel model)
        {
            var expenses = _expense.GetExpensesByDateRange(model).ToList();

            var payments = _payment.GetPaymentByDateRange(model).ToList();

            var revenues = _revenueRepository.GetRevenuesByDateRange(model).ToList();

            var totalExpense = expenses.Sum(x => x.Amount);

            var totalRevenue = payments.Sum(x=> x.Amount) + revenues.Sum(x=> x.Amount);

            var netIncome = totalRevenue - totalExpense;

            var viewModel = new RevenueReportViewModel(expenses, payments, netIncome, totalExpense, totalRevenue)
            {
                Revenues = new List<RevenueReportDataModel>()
            };

            foreach (var payment in payments)
            {
                viewModel.Revenues.Add(new RevenueReportDataModel()
                {
                    Id =payment.Id,
                    Created = payment.Created,                    
                    Amount = payment.Amount
                });
            }

            foreach (var revenue in revenues)
            {
                viewModel.Revenues.Add(new RevenueReportDataModel()
                {
                    Id = revenue.Id,
                    Created = revenue.Created,
                    Amount = revenue.Amount
                });
            }

            viewModel.Revenues = viewModel.Revenues.OrderBy(x => x.Created).ToList();

            return viewModel;
        }

        public DiscountReportViewModel GetDiscountReport(ReportRequestModel model)
        {
            var cases = _case.GetCasesByDateRange(model).ToList();

            var totalDiscountPercent = cases.Sum(x => x.DiscountPercent);

            var totalDiscountAmount = cases.Sum(x => x.DiscountAmount);


            var viewModel = new DiscountReportViewModel(cases, totalDiscountPercent, totalDiscountAmount);

            return viewModel;
        }

        public CustomerReportViewModel GetCustomerReport(ReportRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
