namespace CcsWeb
{
    using CcsWeb.Models;
    using DataContexts;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using System;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<CcsLocalDbContext>(new Func<CcsLocalDbContext>(CcsLocalDbContext.Create));
            app.CreatePerOwinContext<ApplicationUserManager>(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
            CookieAuthenticationOptions options = new CookieAuthenticationOptions {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            };
            CookieAuthenticationProvider provider = new CookieAuthenticationProvider {
                OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(TimeSpan.FromMinutes(30.0), (manager, user) => user.GenerateUserIdentityAsync(manager))
            };
            options.Provider = provider;
            app.UseCookieAuthentication(options);
            app.UseExternalSignInCookie("ExternalCookie");
        }
    }
}

