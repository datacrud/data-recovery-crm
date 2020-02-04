using System;
using System.Collections.Generic;
using System.Web.Http;
using Project.Security.Models;
using Project.Server.Service;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Permission")]
    public class PermissionController : BaseSecurityController<SecurityModels.Permission>
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service) : base(service)
        {
            _service = service;
        }
        

        [HttpPost]
        [Route("CheckPermission")]
        public IHttpActionResult CheckPermission(RequestModels.PermissionRequestModel model)
        {
            bool isPermitted = _service.CheckPermission(model);

            return Ok(isPermitted);
        }

        [HttpGet]
        [Route("GetListById")]
        public IHttpActionResult GetListById(string request)
        {
            var permissions = _service.GetListById(request);

            return Ok(permissions);
        }        


        [HttpPost]
        [Route("AddList")]
        public IHttpActionResult Post(List<SecurityModels.Permission> models)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            foreach (var model in models)
            {
                model.Id = Guid.NewGuid().ToString();
            }

            return Ok(_service.AddList(models));
        }

        
    }
}
