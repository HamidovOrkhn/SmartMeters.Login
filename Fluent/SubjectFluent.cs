using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent
{
    public class SubjectFluent : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("SUBJECT");
            builder.Property(T => T.ID).HasColumnName("SUBJECTID");
            builder.Property(T => T.Name).HasColumnName("NAME");
            builder.Property(T => T.DivisionID).HasColumnName("DIVISIONID");
        }
    }
}
