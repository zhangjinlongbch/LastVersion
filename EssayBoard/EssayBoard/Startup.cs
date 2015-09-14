using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EssayBoard.Startup))]
namespace EssayBoard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
