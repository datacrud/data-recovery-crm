using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Project.RequestModel;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Report")]
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        public IHttpActionResult Post(ReportRequestModel model)
        {

            switch (model.ReportType)
            {
                case ReportType.Expense:
                    return Ok(_service.GetExpenseReport(model));
                case ReportType.Revenue:
                    return Ok(_service.GetRevenueReport(model));
                case ReportType.Discount:
                    return Ok(_service.GetDiscountReport(model));
                case ReportType.Customer:
                    return Ok(_service.GetCustomerReport(model));
            }

            return BadRequest();
        }
    }
}
