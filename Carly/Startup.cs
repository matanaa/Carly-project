using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Carly.Startup))]
namespace Carly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
