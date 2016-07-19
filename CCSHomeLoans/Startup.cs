using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CCSHomeLoans.Startup))]
namespace CCSHomeLoans
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
