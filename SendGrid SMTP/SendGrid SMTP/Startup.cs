using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SendGrid_SMTP.Startup))]
namespace SendGrid_SMTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
