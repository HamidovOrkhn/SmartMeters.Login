using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent.Global
{
    public class UserFluentGlobal : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER_GLOBAL");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.HasIndex(T => T.Pin).IsUnique();
            builder.Property(T => T.Pin).IsRequired().HasMaxLength(20).HasColumnName("PIN");
            builder.Property(T => T.DepartmentId).IsRequired().HasColumnName("DEPARTMENTID");
            builder.Property(T => T.Phone).IsRequired().HasMaxLength(50).HasColumnName("PHONE");
            builder.Property(T => T.Position).IsRequired().HasMaxLength(500).HasColumnName("POSITION");
            builder.Property(T => T.Password).IsRequired().HasMaxLength(500).HasColumnName("PASSWORD");
            builder.Property(T => T.Email).IsRequired().HasMaxLength(100).HasColumnName("EMAIL");
            builder.Property(T => T.RToken).HasColumnName("RTOKEN");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
            builder.Property(T => T.Name).IsRequired().HasMaxLength(50).HasColumnName("NAME");
            builder.Property(T => T.Surname).IsRequired().HasMaxLength(50).HasColumnName("SURNAME");
            builder.Property(T => T.IsActive).HasColumnName("ISACTIVE");
        }
    }
}
