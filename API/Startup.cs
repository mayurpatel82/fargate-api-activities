using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace API
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
            var dbServer = Environment.GetEnvironmentVariable("Server");
            var dbUser = Environment.GetEnvironmentVariable("User");
            var dbUserPassword = Environment.GetEnvironmentVariable("Password");
            var clientUrl = Environment.GetEnvironmentVariable("ClientUrl");

            var dbConnection = $"Server={dbServer};Database=Reactivities;User={dbUser};Password={dbUserPassword};Trusted_Connection=False;";

            services.AddDbContext<DataContext>(opt=>{
                opt.UseSqlServer(dbConnection);
            });

            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod()
                            .WithOrigins("http://localhost:3000", $"http://{clientUrl}");
                    });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
