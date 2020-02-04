using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Security.Models;
using Project.Server.Service;

namespace Project.Server.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    [RoutePrefix("api/Resource")]
    public class ResourceController : BaseSecurityController<SecurityModels.Resource>
    {
        private readonly IResourceService _service;

        public ResourceController(IResourceService service) :base(service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("GetPrivateResources")]
        public IHttpActionResult GetPrivateResources()
        {
            var resources = _service.GetAll().Where(x => x.IsPublic == false).OrderBy(x => x.Name);

            return Ok(resources);
        }

        public override IHttpActionResult Get(string request)
        {
            var model = _service.GetById(request);
            return Ok(model);
        }

        public override IHttpActionResult Post(SecurityModels.Resource model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Id = Guid.NewGuid().ToString();
            var add = _service.Add(model);

            return Ok(add);
        }

        public override IHttpActionResult Delete(string request)
        {
            var delete = _service.Delete(request);
            return Ok(delete);
        }
    }
}
