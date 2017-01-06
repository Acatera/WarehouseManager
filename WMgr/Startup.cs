using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WarehouseManager.Startup))]
namespace WarehouseManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
