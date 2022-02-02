using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.User;

namespace SmartMeterControl.Access_MS.Fluent
{
    public class RoleFluent : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("SMMROLE");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.Property(T => T.Title).IsRequired().HasMaxLength(500).HasColumnName("TITLE");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
        }
    }
}
