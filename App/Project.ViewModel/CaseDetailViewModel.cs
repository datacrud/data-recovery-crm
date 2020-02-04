using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class CaseDetailViewModel
    {
        public CaseDetailViewModel(HddInfo model)
        {
            Id = model.Id;
            CaseNo = model.CaseNo;
            CustomerId = model.CustomerId;
            Brand = model.Brand;
            Model = model.Model;
            Capacity = model.Capacity;
            ReceiveDate = model.ReceiveDate;
            DeliveryDate = model.DeliveryDate;
            Status = model.Status.ToString();
            PaidAmount = model.PaidAmount;
            DueAmount = model.DueAmount;
            Created = model.Created;
            Modified = model.Modified;

            InterfaceType = model.InterfaceType;
            Sl = model.Sl;
            RequiredData = model.RequiredData;
            BackupHdd = model.BackupHdd;
            Note = model.Note;
            TotalCost = model.TotalCost;
            DiscountPercent = model.DiscountPercent;
            DiscountAmount = model.DiscountAmount;
        }

        public Guid Id { get; set; }

        public string CaseNo { get; set; }

        public Guid CustomerId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Capacity { get; set; }        

        public DateTime ReceiveDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Status { get; set; }        

        public double PaidAmount { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountAmount { get; set; }

        public double DueAmount { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }


        public string InterfaceType { get; set; }

        public string Sl { get; set; }

        public string RequiredData { get; set; }

        public string BackupHdd { get; set; }

        public string Note { get; set; }

        public double TotalCost { get; set; }
    }

}
