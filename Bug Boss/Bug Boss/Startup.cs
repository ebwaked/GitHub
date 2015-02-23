using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bug_Boss.Startup))]
namespace Bug_Boss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
