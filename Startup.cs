using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using SmartMeterControl.Access_MS.Database;
using SmartMeterControl.Access_MS.Middleware;
using SmartMeterControl.Access_MS.Resources.Concrete;
using SmartMeterControl.Access_MS.Resources.Interfaces;
using SmartMeterControl.Access_MS.Resources.Libs;
using System.Text;

namespace SmartMeterControl.Access_MS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory =
                    ModelStateValidator.ValidateModelState;
            }).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver = new LowercaseContractResolver());

            services.AddDbContext<DataContext>(T => T.UseOracle(Configuration.GetConnectionString("OracleConnection")));
            services.AddDbContext<GlobalDataContext>(T => T.UseOracle(Configuration.GetConnectionString("OracleConnectionTest"), options => options.UseOracleSQLCompatibility("11")));
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartMeterControl.Access_MS", Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Elnur Hasanzade",
                        Email = "hasanzadeelnur@gmail.com"
                    },
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = false,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });

            //Services
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IGlobalAuthServices, GlobalAuthServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartMeterControl.Access_MS v1"));
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
