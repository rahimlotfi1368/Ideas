using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.EFConfigurations.Configurations;
using Services.EFConfigurations.Configurations.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFConfigurations
{
    public class DataBaseContext:IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions options):base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(MenuConfig).Assembly);
        }
        public DbSet<MenuAccess> MenuAccesses { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
