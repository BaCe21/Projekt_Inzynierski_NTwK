using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TrikProjekt56.Areas.Identity.IdentityHostingStartup))]
namespace TrikProjekt56.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}