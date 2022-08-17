using FreeCourse.Services.Basket.Service;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

    
        public void ConfigureServices(IServiceCollection services)
        {
            //Gelen istekte kesinlikle user bilgileri olacak diyerek bir policy in�a ediyoruz.
            var requreAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            // sub olan �dn�n ismini de�i�tirmesini engeller.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                //Appsettings dosyas�nda yer alan �dentityserver urlsine public key denetimi yapar.
                options.Authority = Configuration["IdentityServerUrl"];
                // Gelen jwt i�erisinde resource_basket var m� diye check eder. E�er varsa i�eri al�r.
                options.Audience = "resource_basket";
                // Https i kapat�r.
                options.RequireHttpsMetadata = false;



            });

            // Bunun sayesinde ald���m�z ve i�lemlerde kulland���m�z jwt token� shared class librarysine g�nderebilece�iz ve oradan user�n �ds�n� ve di�er verilerini �ekebilece�iz.
            // Bunu istersek i� yap�m�zda istersek buradan �ekebiliriz. Her ap�'m�z i�in ayr� ayr� almak yerine shareda bir kere yazmak ve her ap� i�in ordan �ekmek daha kolay geldi�i i�in
            // bu y�ntem tercih edilmi�tir.
            services.AddHttpContextAccessor();

            // ISharedIdentityService ile jwtde de kullan�lan sub yan� user�d k�sm�n� almak i�in bu serviceyi kullan�yoruz.
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.AddScoped<IBasketService, BasketService>();

            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSettings.Host, redisSettings.Port);

                redis.Connect();
                return redis;
            });

     
            services.AddAuthorization();

            // Olu�turdu�umuz policy'i burada filter olarak ekliyoruz.
            services.AddControllers(opt =>
            {
                opt.Filters.Add(new AuthorizeFilter(requreAuthorizePolicy));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Basket", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Basket v1"));
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
