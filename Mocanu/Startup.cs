using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mocanu.Startup))]
namespace Mocanu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
