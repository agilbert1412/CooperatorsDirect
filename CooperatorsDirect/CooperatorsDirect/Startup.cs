using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CooperatorsDirect.Startup))]
namespace CooperatorsDirect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
