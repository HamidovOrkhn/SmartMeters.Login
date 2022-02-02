using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartMeterControl.Access_MS.DTO.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Fluent.Global
{
    public class GlobalLoginRequestDtoFluent : IEntityTypeConfiguration<GlobalLoginRequestDto>
    {
        public void Configure(EntityTypeBuilder<GlobalLoginRequestDto> builder)
        {
            builder.Property(T => T.Pin).IsRequired().HasMaxLength(20).HasColumnName("FIN");
            builder.Property(T => T.Password).IsRequired().HasMaxLength(500).HasColumnName("PASSWORD");
        }
    }
}
