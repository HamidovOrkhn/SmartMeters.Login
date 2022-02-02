using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.User;

namespace SmartMeterControl.Access_MS.Fluent
{
    public class RolePermFluent : IEntityTypeConfiguration<RolePerm>
    {
        public void Configure(EntityTypeBuilder<RolePerm> builder)
        {
            builder.ToTable("SMMROLEPERM");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.Property(T => T.RoleId).HasColumnName("ROLEID");
            builder.Property(T => T.PermissionId).HasColumnName("PERMISSIONID");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
        }
    }
}
