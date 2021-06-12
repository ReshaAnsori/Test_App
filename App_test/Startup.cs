using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App_test.Startup))]
namespace App_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
