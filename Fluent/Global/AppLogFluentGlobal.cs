using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent.Global
{
    public class AppLogFluentGlobal : IEntityTypeConfiguration<AppLog>
    {
        public void Configure(EntityTypeBuilder<AppLog> builder)
        {
            builder.ToTable("USER_LOG_GL");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.Property(T => T.UserId).HasColumnName("USERID");
            builder.Property(T => T.ActionType).HasMaxLength(200).HasColumnName("ACTIONTYPE");
            builder.Property(T => T.DDate).HasColumnName("DDATE");
            builder.Property(T => T.IpAddress).HasMaxLength(50).HasColumnName("IPADDRESS");
            builder.Property(T => T.PermissionId).HasColumnName("PERMISSIONID");
        }
    }
}
