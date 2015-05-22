using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NotreProjet.Startup))]
namespace NotreProjet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
