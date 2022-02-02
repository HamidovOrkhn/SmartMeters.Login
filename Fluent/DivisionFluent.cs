using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent
{
    public class DivisionFluent : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.ToTable("TBL_DIVISIONS");
            builder.Property(T => T.Id).HasColumnName("ID_SEQ");
            builder.Property(T => T.Name).HasColumnName("NAME");
        }
    }
}
