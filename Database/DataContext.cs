using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartMeterControl.Access_MS.Fluent;
using SmartMeterControl.Access_MS.Models;
using SmartMeterControl.Access_MS.Models.User;
using System;

namespace SmartMeterControl.Access_MS.Database
{
    public class DataContext:DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DbContext> options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            if (!options.IsConfigured)
            {
                options.UseOracle(configuration.GetConnectionString("OracleConnection"), options => options.UseOracleSQLCompatibility("11"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserFluent());
            builder.ApplyConfiguration(new RoleFluent());
            builder.ApplyConfiguration(new PermissionFluent());
            builder.ApplyConfiguration(new DepartmentFluent());
            builder.ApplyConfiguration(new RolePermFluent());
            builder.ApplyConfiguration(new DivisionFluent());
            builder.ApplyConfiguration(new SubjectFluent());
        }


        public DbSet<User> SmartMeterUsers { get; set; }
        public DbSet<Role> SmartMeterRoles { get; set; }
        public DbSet<Permission> SmartMeterPermissions { get; set; }
        public DbSet<Department> SmartMeterDepartments { get; set; }
        public DbSet<RolePerm> SmartMeterRolesPerms { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
