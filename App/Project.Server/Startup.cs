using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Project.Security.Startup;

[assembly: OwinStartup(typeof(Project.Server.Startup))]

namespace Project.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new AuthStartup().ConfigureAuth(app);
        }
    }
}
