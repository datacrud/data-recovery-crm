using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Project.Security.Models;
using Project.Security.Startup;

namespace Project.Security.Helpers
{
    public class OwinContextHelper
    {
        private static HttpRequestMessage HttpRequestMessage
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
                }
                return null;
            }
        }
        private static HttpRequestBase HttpRequestBase
        {
            get
            {
                try
                {
                    if (HttpContext.Current != null)
                    {
                        return new HttpRequestWrapper(HttpContext.Current.Request);
                    }
                }
                catch
                {
                    // ignored
                }

                return null;
            }
        }

        public static IOwinContext OwinContext
        {
            get
            {
                // un comment this if Web API is supported
                if (HttpRequestMessage != null)
                {
                    return OwinHttpRequestMessageExtensions.GetOwinContext(HttpRequestMessage);
                }
                if (HttpRequestBase != null)
                {
                    return HttpContextBaseExtensions.GetOwinContext(HttpRequestBase);
                }
                //throw new NotSupportedException("Getting an Owin Context from the current context is not supported");
                return null;
            }
        }

        public static ApplicationManager ApplicationSignInManager => OwinContext?.Get<ApplicationManager>();

        public static ApplicationUserManager ApplicationUserManager => OwinContext?.Get<ApplicationUserManager>();

        public static ApplicationUser CurrentApplicationUser
        {
            get
            {
                if (OwinContext == null) return null;

                var currentUsername = OwinContext.Authentication.User.Identity.Name;
                using (var context = new ApplicationDbContext())
                {
                    var user = context.Users.FirstOrDefault(o => o.UserName == currentUsername);
                    return user;
                }
            }
        }

    }
}