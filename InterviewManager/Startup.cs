using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InterviewManager.Startup))]
namespace InterviewManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
