using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LastOasis.Startup))]
namespace LastOasis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
