using System;
using Project.Model;

namespace Project.ViewModel
{
    public class RevenuePaymentViewModel
    {
        public RevenuePaymentViewModel(Payment model)
        {
            Created = model.Created;
            Amount = model.Amount;
        }

        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public double Amount { get; set; }
    }
}