using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cool_Thing.Startup))]
namespace Cool_Thing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
