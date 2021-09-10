using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TejInfraFollowUp.Startup))]
namespace TejInfraFollowUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
