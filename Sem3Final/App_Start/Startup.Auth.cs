using Microsoft.AspNet.Identity;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/PageLogin"),
                LogoutPath = new PathString("/Login/Logout"),
                ExpireTimeSpan = TimeSpan.FromMinutes(30.0),
            });
        }
    }
}