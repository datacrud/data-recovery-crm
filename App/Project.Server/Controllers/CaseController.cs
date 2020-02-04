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
    [RoutePrefix("api/Case")]
    public class CaseController : BaseController<HddInfo>
    {
        private readonly ICaseService _service;
        private readonly ICaseLogService _caseLogService;

        public CaseController(ICaseService service, ICaseLogService caseLogService) : base(service)
        {
            _service = service;
            _caseLogService = caseLogService;
        }


        public override IHttpActionResult Get()
        {
            var list = _service.GetAll().ConvertAll(x => new CaseViewModel(x)).OrderByDescending(x => x.Modified).Take(5);

            return Ok(list);
        }

        [HttpGet]
        [Route("GetPrintList")]
        public IHttpActionResult GetPrintList()
        {
            var list = _service.GetAll().ConvertAll(x => new CaseViewModel(x)).OrderByDescending(x => x.CaseNo).Take(10);

            return Ok(list);
        }


        public override IHttpActionResult Get(string request)
        {
            var model = _service.GetById(Guid.Parse(request));
            var viewModel = new CaseDetailViewModel(model);

            return Ok(viewModel);
        }

        public override IHttpActionResult Post(HddInfo model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var lastCreatedCaseCode = _service.LastCreatedCaseCode();
            if (string.IsNullOrWhiteSpace(lastCreatedCaseCode)) return BadRequest();

            model.CaseNo = HelperRequestModel.GetCaseNo((Convert.ToInt32(lastCreatedCaseCode) + 1).ToString());
            model.Created = DateTime.Now;
            model.CreatedBy = User.Identity.GetUserId();
            model.Modified = DateTime.Now;

            var add = _service.Add(model);

            if (!add) return BadRequest();
            AddCaseLog(model);

            return Ok(model.Id);
        }

        public override IHttpActionResult Put(HddInfo model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Modified = DateTime.Now;
            model.ModifiedBy = User.Identity.GetUserId();
            model.DiscountAmount = model.TotalCost * (model.DiscountPercent / 100);

            var edit = _service.Edit(model);
            if (!edit) return BadRequest();

            var updatePriceTotal = _service.UpdatePriceTotal(model.Id);
            if (!updatePriceTotal) return BadRequest();

            return Ok(true);
        }

        [HttpPut]
        [Route("UpdateStatus")]
        public IHttpActionResult UpdateStatus(HddInfo model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Modified = DateTime.Now;
            model.ModifiedBy = User.Identity.GetUserId();
            if(model.Status == Status.Delivered)
                model.DeliveryDate = DateTime.Now;
            var edit = _service.Edit(model);

            if (!edit) return BadRequest();

            var log = AddCaseLog(model);

            return Ok(log);
        }


        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(SearchRequestModel request)
        {
            var list = _service.Search(request);

            return Ok(list);
        }


        private bool AddCaseLog(HddInfo model)
        {
            var caseLog = new CaseLog
            {
                HddInfoId = model.Id,
                Status = model.Status,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };
            return _caseLogService.Add(caseLog);
        }


        [HttpPost]
        [Route("SearchPrintableCase")]
        public IHttpActionResult SearchPrintableCase(SearchRequestModel request)
        {
            List<CaseViewModel> models = _service.SearchPrintableCase(request.Keyword);

            return Ok(models);
        }

    }   
  
}
