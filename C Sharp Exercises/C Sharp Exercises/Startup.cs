using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(C_Sharp_Exercises.Startup))]
namespace C_Sharp_Exercises
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
