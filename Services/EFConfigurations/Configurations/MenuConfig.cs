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
    internal class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            //builder.Property(a => a.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.CreateDate).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(a => a.MenuLinkTitle).IsRequired(true);
            builder.Property(a => a.MenuLinkUrl).IsRequired(true);
            builder.HasIndex(a => a.MenuLinkTitle).IsUnique(true);
            builder.HasIndex(a => a.MenuLinkUrl).IsUnique(true);

            builder.HasMany(a => a.MenuAccesses)
                .WithOne(s => s.Menu)
                .HasForeignKey(a => a.MenuId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(true);
        }
    }

}
