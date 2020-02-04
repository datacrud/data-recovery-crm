using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Project.Model;
using Project.Service;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Payment")]
    public class PaymentController : BaseController<Payment>
    {
        private readonly IPaymentService _service;
        private readonly ICaseService _caseService;

        public PaymentController(IPaymentService service, ICaseService caseService) : base(service)
        {
            _service = service;
            _caseService = caseService;
        }

        [HttpGet]
        [Route("GetListById")]
        public IHttpActionResult GetListById(string request)
        {
            var list = _service.GetListById(Guid.Parse(request));

            return Ok(list);
        }

        public override IHttpActionResult Post(Payment model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Created = DateTime.Now;
            model.CreatedBy = User.Identity.GetUserId();
            model.Modified = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;

            var add = _service.Add(model);

            if (!add) return BadRequest();
            var isUpdated = _caseService.UpdatePriceTotal(model.HddInfoId);

            if (!isUpdated) return BadRequest();

            return Ok(model.Id);
        }

        public override IHttpActionResult Put(Payment model)
        {
            if (!ModelState.IsValid) return BadRequest();

            model.Modified = DateTime.Now;
            model.ModifiedBy = User.Identity.GetUserId();

            var edit = _service.Edit(model);
            if (!edit) return BadRequest();

            var updatePriceTotal = _caseService.UpdatePriceTotal(model.HddInfoId);
            if (!updatePriceTotal) return BadRequest();

            return Ok(model.Id);
        }


        public override IHttpActionResult Delete(string request)
        {
            var delete = _service.Delete(Guid.Parse(request));
            if (!delete) return BadRequest();            

            return Ok(true);
        }
    }
}
