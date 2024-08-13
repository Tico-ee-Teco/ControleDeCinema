using ControleDeCinema.Dominio;
using ControleDeCinema.Dominio.Compartilhado;
using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFilme;
using ControleDeCinema.Infra.ModuloFuncionario;
using ControleDeCinema.Infra.ModuloGenero;
using ControleDeCinema.Infra.ModuloSala;
using ControleDeCinema.Infra.ModuloSessao;

namespace ControleDeCinema.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ControleDeCinemaDbContext>();
            
            builder.Services.AddScoped<IRepositorioSala, RepositorioSalaEmOrm>();
            builder.Services.AddScoped<IRepositorioFilme, RepositorioFilmeEmOrm>();
            builder.Services.AddScoped<IRepositorioGenero, RepositorioGeneroEmOrm>();
            builder.Services.AddScoped<IRepositorioSessao, RepositorioSessaoEmOrm>();
            builder.Services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmOrm>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Inicio}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
