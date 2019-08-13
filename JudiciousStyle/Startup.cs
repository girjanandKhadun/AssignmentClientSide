using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JudiciousStyle.Startup))]
namespace JudiciousStyle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
