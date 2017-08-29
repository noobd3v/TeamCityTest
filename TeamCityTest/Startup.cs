using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamCityTest.Startup))]
namespace TeamCityTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
