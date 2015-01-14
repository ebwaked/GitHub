using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Resume_MIO.Startup))]
namespace MVC_Resume_MIO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
