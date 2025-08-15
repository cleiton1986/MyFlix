using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFlix.AppServer.Interfaces;
using MyFlix.Repository;
using MyFlix.Repository.Interfaces;
using MyFlix.Serve;
using MyFlix.Serve.Interfaces;

namespace MyFlix.AppServer.Configuracao
{
    public class Bootstrap
    {
        public static void AddServices(IServiceCollection services, IConfiguration configure)
        {

            services.AddMvcCore().AddApiExplorer();
            services.AddRazorPages();
            services.AddMvc();

            services.AddSingleton<IConfiguration>(configure);

            AddRepository(services);
            AddAppServe(services);
        }

        public static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IFilmesRepositorio, FilmesRepositorio>();
            services.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
        }

        public static void AddAppServe(IServiceCollection services)
        {
            services.AddTransient<IFilmesAppServe, FilmesAppServe>();
            services.AddTransient<IAvaliacaoAppServe, AvaliacaoAppServe>();
            services.AddTransient<IAvaliacaoServe, AvaliacaoServe>();
            services.AddTransient<IFilmesServe, FilmesServe>();

        }
    }
}
