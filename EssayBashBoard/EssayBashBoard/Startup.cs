using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EssayBashBoard.Startup))]
namespace EssayBashBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
