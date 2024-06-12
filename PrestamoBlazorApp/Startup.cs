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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor;
using MudBlazor.Services;
using PrestamoBlazorApp.Services.Pruebas;

using PrestamoBlazorApp.Providers;
using Blazored.LocalStorage;
using PrestamoBlazorApp.Services.BaseService;
using DispatchAPI.Authentication.Services;

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
           
            services.AddRazorPages(); // estaba antes debajo de addHttpClient();
            services.AddServerSideBlazor();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddServerSideBlazor().AddHubOptions(options =>
            {
                // maximum message size of 2MB
                options.MaximumReceiveMessageSize = (1024*1024*5);
            });
          
            AddLibsServices(services);

            //services.AddServerSideBlazor();
           
            AddDevCoreServices(services);
            
            ProjectServices(services);
            AddMudBlazorServices(services);
        }

        private static void ProjectServices(IServiceCollection services)
        {
            
            services.AddScoped<CommonInjectionsService>();
            services.AddScoped<IngresosService>();
            services.AddScoped<ColoresService>();
            services.AddScoped<MarcasService>();
            services.AddScoped<ModelosService>();
            services.AddScoped<ClientesService>();
            services.AddScoped<ClasificacionesService>();
            services.AddScoped<EquiposService>();
            //services.AddScoped<OcupacionesService>();
            services.AddScoped<CatalogosServicesFactoryManager>();
            services.AddScoped<ISiteResourcesService, SiteResourcesService>();
            //services.AddScoped<ColoresServiceV2>();
            //services.AddScoped<OcupacionesServiceV2>();
            //services.AddScoped<TiposSexoService>();
            services.AddScoped<GarantiasService>();
            services.AddScoped<LocalidadesService>();
            services.AddScoped<TipoGarantiaService>();
            services.AddScoped<TiposMoraService>();
            services.AddScoped<DivisionTerritorialService>();
            services.AddScoped<TasasInteresService>();
            services.AddScoped<PeriodosService>();
            services.AddScoped<PrestamosService>();
            services.AddScoped<ReportesService>();
            services.AddScoped<SetParametrosService>();
            services.AddScoped<CodeudoresService>();
            services.AddScoped<TestService>();
            services.AddScoped<LocalidadesNegociosService>();
            services.AddScoped<EstatusService>();
            services.AddScoped<ClientesEstatusService>();
            services.AddScoped<LocalizadoresService>();
            services.AddScoped<PrestamosEstatusService>();
            services.AddScoped<NegociosService>();
            services.AddScoped<ServicioPruebas>();
            services.AddScoped<AuthService>();
            services.AddScoped<ServiceBase>();
            services.AddScoped<CustomService>();
            services.AddScoped<CodigosCargosDebitosService>();



            //services.AddSingleton<IServicioPruebas, ServicioPruebas>();


        }
        private void AddDevCoreServices(IServiceCollection services)
        {
            services.AddScoped<SystemService>();
            services.AddScoped<ActionsManagerService>();
            services.AddScoped<UserManagerService>();
            //services.AddScoped<DiasFeriadosService>();
            services.AddScoped<SystemPoliciesService>();
        }
        private void AddLibsServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options =>
            {
                //if (Env.IsDevelopment())
                //{
                options.DetailedErrors = true;
                //}
            });

            //services.AddServerSideBlazor();
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<TokenAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());
            services.AddHttpContextAccessor();
            services.AddBlazoredLocalStorage();
            // todo 20230219 chequear si esto es o no necesario el Tls13
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;

            services.AddHttpClient();

            services.AddSingleton<NotificationService>();
        }
        private static void AddMudBlazorServices(IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 4000;
                config.SnackbarConfiguration.HideTransitionDuration = 600;
                config.SnackbarConfiguration.ShowTransitionDuration = 600;
                config.SnackbarConfiguration.ClearAfterNavigation = true;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
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

            app.UseMiddleware<MenuMiddleware>();
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

