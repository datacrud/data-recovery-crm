using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Model;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/CaseLog")]
    public class CaseLogController : BaseController<CaseLog>
    {
        private readonly ICaseLogService _service;

        public CaseLogController(ICaseLogService service) : base(service)
        {
            _service = service;
        }


        [Route("GetCaseLog")]
        public IHttpActionResult GetCaseLog(string request)
        {
            var list = _service.GetCaseLog(Guid.Parse(request));

            return Ok(list);
        }
    }
}
