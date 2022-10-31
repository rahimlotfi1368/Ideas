using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFConfigurations.Configurations
{
    internal class MenuAccessConfig : IEntityTypeConfiguration<MenuAccess>
    {
        public void Configure(EntityTypeBuilder<MenuAccess> builder)
        {
            
            builder.HasKey(a=> new { a.RoleId, a.MenuId });
            
        }
    }
}
