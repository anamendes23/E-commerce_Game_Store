using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcCandyBotGames.Startup))]
namespace MvcCandyBotGames
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
