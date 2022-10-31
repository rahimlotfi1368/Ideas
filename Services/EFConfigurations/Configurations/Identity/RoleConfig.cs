using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFConfigurations.Configurations.Identity
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(a => a.MenuAccesses)
              .WithOne(a=>a.Role)
              .HasForeignKey(a => a.RoleId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired(true);
        }
    }
}
