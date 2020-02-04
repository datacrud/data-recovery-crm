using System;
using Project.Model;

namespace Project.ViewModel
{
    public class RevenueViewModel
    {
        public RevenueViewModel(Revenue model)
        {
            Id = model.Id;
            Date = model.Date;
            Created = model.Created;
            Description = model.Description;
            Amount = model.Amount;
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
    }
}