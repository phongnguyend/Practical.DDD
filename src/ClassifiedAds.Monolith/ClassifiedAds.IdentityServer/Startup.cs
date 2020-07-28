﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ClassifiedAds.Domain.Entities;
using ClassifiedAds.IdentityServer.ConfigurationOptions;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClassifiedAds.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        public IConfiguration Configuration { get; }

        private AppSettings AppSettings { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddCors();

            services.AddPersistence(AppSettings.ConnectionStrings.ClassifiedAds)
                    .AddDomainServices()
                    .AddApplicationServices((Type serviceType, Type implementationType, ServiceLifetime serviceLifetime) =>
                    {
                        services.AddInterceptors(serviceType, implementationType, serviceLifetime, AppSettings.Interceptors);
                    })
                    .AddMessageHandlers()
                    .ConfigureInterceptors()
                    .AddIdentity();

            services.AddIdentityServer()
                    .AddSigningCredential(new X509Certificate2(AppSettings.Certificates.Default.Path, AppSettings.Certificates.Default.Password))
                    .AddAspNetIdentity<User>()
                    .AddIdServerPersistence(AppSettings.ConnectionStrings.ClassifiedAds);

            services.AddCaches(AppSettings.Caching)
                    .AddClassifiedAdsProfiler(AppSettings.Monitoring);

            services.AddAuthentication()
                    .AddOpenIdConnect("AAD", "Azure Active Directory", options =>
                    {
                        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                        options.Authority = AppSettings.AzureActiveDirectory.Authority;
                        options.ClientId = AppSettings.AzureActiveDirectory.ClientId;
                        options.ClientSecret = AppSettings.AzureActiveDirectory.ClientSecret;
                        options.ResponseType = "code id_token";
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.Scope.Add("email");
                        options.Scope.Add("offline_access");
                        options.SaveTokens = true;
                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.Events.OnTicketReceived = (ct) =>
                        {
                            return Task.CompletedTask;
                        };
                        options.Events.OnTokenResponseReceived = (ct) =>
                        {
                            return Task.CompletedTask;
                        };
                        options.Events.OnTokenValidated = (ct) =>
                        {
                            return Task.CompletedTask;
                        };
                        options.Events.OnUserInformationReceived = (ct) =>
                        {
                            return Task.CompletedTask;
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseSecurityHeaders(AppSettings.SecurityHeaders);

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseClassifiedAdsProfiler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
