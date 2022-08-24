using FreeCourse.Services.Order.Application.Consumer;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Services;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.Services.Order.API
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

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderMessageCommandConsumer>();
                x.AddConsumer<CourseNameChangedEventConsumer>();
                // Default Port : 5672
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint("create-order-service", e =>
                    {
                        e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
                    });
                    cfg.ReceiveEndpoint("course-name-changed-event-order-service", e =>
                    {
                        e.ConfigureConsumer<CourseNameChangedEventConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();





            //Gelen istekte kesinlikle user bilgileri olacak diyerek bir policy in�a ediyoruz.
            var requreAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            // sub olan �dn�n ismini de�i�tirmesini engeller.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //Appsettings dosyas�nda yer alan �dentityserver urlsine public key denetimi yapar.
                options.Authority = Configuration["IdentityServerUrl"];
                // Gelen jwt i�erisinde resource_basket var m� diye check eder. E�er varsa i�eri al�r.
                options.Audience = "resource_order";
                // Https i kapat�r.
                options.RequireHttpsMetadata = false;



            });

            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),configure =>
                {
                    configure.MigrationsAssembly("FreeCourse.Services.Order.Infrastructure");
                });
            });
            services.AddAuthentication();
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(FreeCourse.Services.Order.Application.Mapping.ObjectMapper).Assembly);
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            // Olu�turdu�umuz policy'i burada filter olarak ekliyoruz.
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requreAuthorizePolicy));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Order.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Order.API v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
