using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Error404_Web.Startup))]
namespace Error404_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
