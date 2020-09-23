using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADManageSystem.Startup))]
namespace ADManageSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
