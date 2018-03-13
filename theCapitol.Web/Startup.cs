using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(theCapitol.Web.Startup))]
namespace theCapitol.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
