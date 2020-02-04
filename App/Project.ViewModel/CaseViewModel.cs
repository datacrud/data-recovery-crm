using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class CaseViewModel
    {
        public CaseViewModel(HddInfo model)
        {
            Id = model.Id;
            CaseNo = model.CaseNo;
            Brand = model.Brand;
            Model = model.Model;
            Capacity = model.Capacity;
            Sl = model.Sl;
            ReceiveDate = model.ReceiveDate;
            DeliveryDate = model.DeliveryDate;
            Status = model.Status.ToString();
            PaidAmount = model.PaidAmount;
            DueAmount = model.DueAmount;

            CustomerId = model.CustomerId;
            InterfaceType = model.InterfaceType;
            RequiredData = model.RequiredData;
            BackupHdd = model.BackupHdd;            

            Created = model.Created;
            Modified = model.Modified;
        }

        public Guid Id { get; set; }

        public string CaseNo { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Capacity { get; set; }

        public string Sl { get; set; }

        public DateTime ReceiveDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Status { get; set; }

        public double PaidAmount { get; set; }

        public double DueAmount { get; set; }



        public Guid CustomerId { get; set; }

        public string InterfaceType { get; set; }

        public string RequiredData { get; set; }

        public string BackupHdd { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
