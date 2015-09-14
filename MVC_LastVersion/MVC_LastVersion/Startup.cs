using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_LastVersion.Startup))]
namespace MVC_LastVersion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
