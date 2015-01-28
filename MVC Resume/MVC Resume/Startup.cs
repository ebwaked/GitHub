using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Resume.Startup))]
namespace MVC_Resume
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
