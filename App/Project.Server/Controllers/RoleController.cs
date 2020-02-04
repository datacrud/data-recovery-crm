using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Server.Service;

namespace Project.Server.Controllers
{
    [Authorize(Roles = "SystemAdmin, Admin")]
    [RoutePrefix("api/Role")]
    public class RoleController : BaseSecurityController<IdentityRole>
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service) :base(service)
        {
            _service = service;
        }

        public override IHttpActionResult Get(string request)
        {
            var model = _service.GetById(request);
            return Ok(model);
        }

        public override IHttpActionResult Post(IdentityRole model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Id = Guid.NewGuid().ToString();
            var add = _service.Add(model);

            return Ok(add);
        }

        public override IHttpActionResult Delete(string request)
        {
            var model = _service.Delete(request);
            return Ok(model);
        }
    }
}
