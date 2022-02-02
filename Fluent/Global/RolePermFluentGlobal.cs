using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent.Global
{
    public class RolePermFluentGlobal : IEntityTypeConfiguration<RolePerm>
    {
        public void Configure(EntityTypeBuilder<RolePerm> builder)
        {
            builder.ToTable("ROLEPERM_GLOBAL");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.Property(T => T.RoleId).HasColumnName("ROLEID");
            builder.Property(T => T.UserId).HasColumnName("USERID");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
        }
    }
}
