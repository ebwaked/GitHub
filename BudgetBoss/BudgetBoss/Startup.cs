using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetBoss.Startup))]
namespace BudgetBoss
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
