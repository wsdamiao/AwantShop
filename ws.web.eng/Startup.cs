using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ws.web.eng.Startup))]
namespace ws.web.eng
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
