using System.Web.Http;
using Project.Security.Models;
using Project.Server.Service;

namespace Project.Server.Controllers
{
    [Authorize]
    [RoutePrefix("api/Profile")]
    public class ProfileController : BaseSecurityController<ApplicationUser>
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service) : base(service)
        {
            _service = service;
        }

        //Profile Section
        [HttpGet]
        [Route("UserProfile")]
        public IHttpActionResult GetUserProfile()
        {
            var model = _service.GetUserProfile();
            return Ok(model);
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public IHttpActionResult UpdateProfile(RequestModels.UserProfileUpdateRequestModel model)
        {
            return Ok(_service.UpdateProfile(model));
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public IHttpActionResult UpdatePassword(RequestModels.ChangePasswordRequestModel model)
        {
            return Ok(_service.UpdatePassword(model));
        }
    }
}
