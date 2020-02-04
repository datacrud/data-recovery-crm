using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.RequestModel
{
    public class SearchRequestModel
    {
        public string Keyword { get; set; }

        public Status Filter { get; set; }

        public DateTime StartDate { get; set; }
    }
}
