using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CRCtoUSDService.Startup))]

namespace CRCtoUSDService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}