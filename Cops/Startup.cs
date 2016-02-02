using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cops.Startup))]
namespace Cops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
