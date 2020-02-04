using System;
using Project.Model;

namespace Project.ViewModel
{
    public class ExpenseViewModel
    {
        public ExpenseViewModel(Expense model)
        {
            Id = model.Id;
            Created = model.Created;
            Description = model.Description;
            Amount = model.Amount;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }
    }
}