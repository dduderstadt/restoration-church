using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rcliberty.Web.Startup))]
namespace rcliberty.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
