using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventManagement.Startup))]
namespace EventManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
