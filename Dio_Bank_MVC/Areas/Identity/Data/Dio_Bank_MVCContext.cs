using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dio_Bank_MVC.Areas.Identity.Data;
using Dio_Bank_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dio_Bank_MVC.Data
{
    public class Dio_Bank_MVCContext : IdentityDbContext<Dio_Bank_MVCUser>
    {
        public virtual DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public Dio_Bank_MVCContext(DbContextOptions<Dio_Bank_MVCContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
