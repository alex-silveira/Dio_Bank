using System;
using Dio_Bank_MVC.Areas.Identity.Data;
using Dio_Bank_MVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Dio_Bank_MVC.Areas.Identity.IdentityHostingStartup))]
namespace Dio_Bank_MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Dio_Bank_MVCContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Dio_Bank_MVCContextConnection")));

                services.AddDefaultIdentity<Dio_Bank_MVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Dio_Bank_MVCContext>();
            });
        }
    }
}