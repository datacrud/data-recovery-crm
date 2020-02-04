using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Project.Model;
using Project.RequestModel;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Customer")]
    public class CustomerController : BaseController<Customer>
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service) : base(service)
        {
            _service = service;
        }


        public override IHttpActionResult Get()
        {
            var list = _service.GetAll().OrderByDescending(x=> x.Modified).Take(10);
            return Ok(list);
        }

        

        public override IHttpActionResult Post(Customer model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var lastCreatedCustomerCode = _service.GetLastCreatedCustomerCode();
            if (string.IsNullOrWhiteSpace(lastCreatedCustomerCode)) return BadRequest();

            model.Code = HelperRequestModel.GetCustomerCode((Convert.ToInt32(lastCreatedCustomerCode) + 1).ToString());
            model.Created = DateTime.Now;
            model.CreatedBy = User.Identity.GetUserId();
            model.Modified = DateTime.Now;

            var add = _service.Add(model);

            return Ok(add);
        }

        public override IHttpActionResult Put(Customer model)
        {
            if (!ModelState.IsValid) return BadRequest();

            model.Modified = DateTime.Now;
            model.ModifiedBy = User.Identity.GetUserId();

            var edit = _service.Edit(model);

            return Ok(model.Id);
        }

        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(SearchRequestModel model)
        {
            var list = _service.Search(model);

            return Ok(list);
        }

        [HttpGet]
        [Route("CustomerDropdownList")]
        public IHttpActionResult CustomerDropdownList()
        {
            List<DropdownListViewModel> models = _service.GetAll().ConvertAll(x=> new DropdownListViewModel()
            {
                Id = x.Id,
                Name = x.Code
            });

            return Ok(models.OrderByDescending(x=> x.Name));
        }
    }
}
