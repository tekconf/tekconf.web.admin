using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TekConf.Web.Admin.Startup))]
namespace TekConf.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
