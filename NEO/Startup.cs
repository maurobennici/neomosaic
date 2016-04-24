using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NEO.Startup))]
namespace NEO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
