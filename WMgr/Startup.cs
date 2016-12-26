using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMgr.Startup))]
namespace WMgr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
