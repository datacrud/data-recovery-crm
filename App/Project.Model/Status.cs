using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public enum Status
    {
        All,
        Received,
        Inspected,
        Quoted,
        Approved,        
        Processing,
        Pending,
        Delivered,
        Canceled
    }
}
