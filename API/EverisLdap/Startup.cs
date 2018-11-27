using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using EverisLdap.Repositories;
using EverisLdap.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace EverisLdap
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
            services.AddCors();
            services.AddTransient<IEverisLdapRepository,EverisLdapRepository>();
            // Configure constants.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            
            services.Configure<AppSettings>(appSettingsSection);
            services.AddAuthorization( options => {
                options.AddPolicy("tokenValidation", policy => {
                    policy.Requirements.Add(new TokenRequirement());
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    });
                options.AddPolicy("Administrator", policy => {
                    policy.Requirements.Add(new RolRequirement(Utils.Rol.Administrator.ToString()));
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    });
                options.AddPolicy("Supervisor", policy => {
                    policy.Requirements.Add(new RolRequirement(Utils.Rol.Supervisor.ToString()));
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    });
            });
            services.AddAuthentication().AddJwtBearer();
            services.AddSingleton<IAuthorizationHandler,TokenHandler>();
            services.AddSingleton<IAuthorizationHandler,RolHandler>();
            services.AddHttpContextAccessor();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory){
            if (!env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
            else{
                app.UseHsts();
            }
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
