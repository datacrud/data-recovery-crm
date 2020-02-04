using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Project.Model;
using Project.RequestModel;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Revenue")]
    public class RevenueController : BaseController<Revenue>
    {
        private readonly IRevenueService _service;

        public RevenueController(IRevenueService service) : base(service)
        {
            _service = service;
        }

        public override IHttpActionResult Post(Revenue model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Date = DateTime.Now;
            model.Created = DateTime.Now;
            model.Modified = DateTime.Now;
            model.CreatedBy = User.Identity.GetUserId();

            var add = _service.Add(model);

            return Ok(add);
        }


        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(SearchRequestModel request)
        {
            List<RevenueViewModel> list = _service.Search(request);
            return Ok(list);
        }
       
    }
}
