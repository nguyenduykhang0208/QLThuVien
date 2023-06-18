using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThuVien.Startup))]
namespace ThuVien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
