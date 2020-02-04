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
    [RoutePrefix("api/Expense")]
    public class ExpenseController : BaseController<Expense>
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service) : base(service)
        {
            _service = service;
        }

        public override IHttpActionResult Post(Expense model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Created = DateTime.Now;
            model.CreatedBy = User.Identity.GetUserId();
            model.Modified = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;

            var add = _service.Add(model);

            return Ok(add);
        }


        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(SearchRequestModel request)
        {
            List<ExpenseViewModel> list = _service.Search(request);
            return Ok(list);
        }
       
    }
}
