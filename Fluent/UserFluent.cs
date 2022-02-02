using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models.User;

namespace SmartMeterControl.Access_MS.Fluent
{
    public class UserFluent : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("SMMUSER");
            builder.HasKey(T => T.Id);
            builder.Property(T => T.Id).HasColumnName("ID");
            builder.HasIndex(T => T.Username).IsUnique();
            builder.Property(T => T.Username).IsRequired().HasMaxLength(50).HasColumnName("USERNAME");
            builder.Property(T => T.FullName).IsRequired().HasMaxLength(100).HasColumnName("FULLNAME");
            builder.Property(T => T.DivisionId).IsRequired().HasMaxLength(100).HasColumnName("REGIONID");
            builder.Property(T => T.SubjectId).IsRequired().HasMaxLength(100).HasColumnName("SUBJECTID");
            builder.Property(T => T.DepartmentId).IsRequired().HasColumnName("DEPARTMENTID");
            builder.Property(T => T.Phone).IsRequired().HasMaxLength(50).HasColumnName("PHONE");
            builder.Property(T => T.Position).IsRequired().HasMaxLength(500).HasColumnName("POSITION");
            builder.Property(T => T.Password).IsRequired().HasMaxLength(500).HasColumnName("PASSWORD");
            builder.Property(T => T.Email).IsRequired().HasMaxLength(100).HasColumnName("EMAIL");
            builder.Property(T => T.RoleId).IsRequired().HasColumnName("ROLEID");
            builder.Property(T => T.RToken).HasColumnName("RTOKEN");
            builder.Property(T => T.CreatedDate).HasColumnName("CREATEDDATE");
            builder.Property(T => T.UpdatedDate).HasColumnName("UPDATEDDATE");
        }
    }
}
