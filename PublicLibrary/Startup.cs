using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublicLibrary.Startup))]
namespace PublicLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
