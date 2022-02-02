using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent.Global
{
    public class RoleFluentGlobal : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("ROLE_GLOBAL");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.Property(T => T.Title).IsRequired().HasMaxLength(500).HasColumnName("TITLE");
            builder.Property(T => T.SiteUrl).IsRequired().HasMaxLength(500).HasColumnName("SITEURL");
            builder.Property(T => T.IsHttps).HasColumnName("ISHTTPS");
            builder.Property(T => T.ImageUrl).HasColumnName("IMAGEURL");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
        }
    }
}
