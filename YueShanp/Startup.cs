using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YueShanp.Startup))]
namespace YueShanp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
