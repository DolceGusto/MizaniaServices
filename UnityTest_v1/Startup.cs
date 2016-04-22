using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnityTest_v1.Startup))]
namespace UnityTest_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
