using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartMeterControl.Access_MS.Fluent;
using SmartMeterControl.Access_MS.Fluent.Global;
using SmartMeterControl.Access_MS.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterControl.Access_MS.Database
{
    public class GlobalDataContext : DbContext
    {
        public GlobalDataContext()
        {

        }

        public GlobalDataContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            if (!options.IsConfigured)
            {
                options.UseOracle(configuration.GetConnectionString("OracleConnectionTest"), options => options.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
                builder.Entity<User>()
            .HasMany(c => c.RolePerms)
            .WithOne(e => e.User)
            .HasConstraintName("U_USER_FK");
                builder.Entity<Role>()
            .HasMany(c => c.RolePerms)
            .WithOne(e => e.Role)
            .HasConstraintName("U_ROLE_FK");

            builder.ApplyConfiguration(new UserFluentGlobal());
            builder.ApplyConfiguration(new RoleFluentGlobal());
            builder.ApplyConfiguration(new RolePermFluentGlobal());
            builder.ApplyConfiguration(new AppLogFluentGlobal());
        }


        public DbSet<User> GlobalUsers { get; set; }
        public DbSet<Role> GlobalRoles { get; set; }
        public DbSet<RolePerm> GlobalRolesPerms { get; set; }
        public DbSet<AppLog> AppLogs { get; set; }
    }
}
