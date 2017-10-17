using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(teenBook.Startup))]
namespace teenBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
