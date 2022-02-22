using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrestamoBlazorApp.Areas.Identity;
using PrestamoBlazorApp.Services;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor;
using MudBlazor.Services;

namespace PrestamoBlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddHttpClient();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            RadZenServices(services);

            ProjectServices(services);

            AddMudBlazorServices(services);
        }

        private static void RadZenServices(IServiceCollection services)
        {
            services.AddScoped<Radzen.DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
        }

        private static void ProjectServices(IServiceCollection services)
        {
            services.AddSingleton<CatalogosService>();
            services.AddSingleton<IngresosService>();
            services.AddSingleton<ColoresService>();
            services.AddSingleton<MarcasService>();
            services.AddSingleton<ModelosService>();
            services.AddSingleton<ClientesService>();
            services.AddSingleton<ClasificacionesService>();
            services.AddSingleton<EquiposService>();
            services.AddSingleton<OcupacionesService>();
            services.AddSingleton<GarantiasService>();
            services.AddSingleton<LocalidadesService>();
            services.AddSingleton<TipoGarantiaService>();
            services.AddSingleton<TiposMoraService>();
            services.AddSingleton<TerritoriosService>();
            services.AddSingleton<TasasInteresService>();
            services.AddSingleton<PeriodosService>();
            services.AddSingleton<PrestamosService>();
            services.AddSingleton<ReportesService>();
            services.AddSingleton<SetParametrosService>();
            services.AddSingleton<CodeudoresService>();
            services.AddSingleton<TestService>();
            services.AddSingleton<LocalidadesNegociosService>();
        }

        private static void AddMudBlazorServices(IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 20000;
                config.SnackbarConfiguration.HideTransitionDuration = 600;
                config.SnackbarConfiguration.ShowTransitionDuration = 600;
                //config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

