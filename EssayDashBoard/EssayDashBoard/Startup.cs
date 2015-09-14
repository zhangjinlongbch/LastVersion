using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EssayDashBoard.Startup))]
namespace EssayDashBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
