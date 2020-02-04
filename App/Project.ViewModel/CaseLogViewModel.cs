using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public class CaseLogViewModel
    {
        public CaseLogViewModel(CaseLog model)
        {
            Status = model.Status.ToString();
            Created = model.Created;
        }

        public string Status { get; set; }

        public DateTime Created { get; set; }
    }
}
