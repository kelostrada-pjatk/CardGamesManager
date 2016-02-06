using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CardGames.MVC.Startup))]
namespace CardGames.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
