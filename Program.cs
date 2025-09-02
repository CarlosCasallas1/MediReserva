using MediReserva.Components;
using MediReserva.Data;
using MediReserva.Services.Implementation;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
namespace MediReserva
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1️⃣ Registrar servicios de MVC/API
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 2️⃣ Registrar servicios de aplicación
            builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
            builder.Services.AddScoped<IConsultorioService, ConsultorioService>();
            builder.Services.AddScoped<IMedicoService, MedicoService>();
            builder.Services.AddScoped<IPacienteService, PacienteService>();

            // 3️⃣ Configurar DbContext para SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicaDB")));

            // 4️⃣ Registrar Blazor/Razor Components
            builder.Services.AddRazorComponents()
                   .AddInteractiveServerComponents();

            var app = builder.Build();

            // 5️⃣ Configurar pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // 6️⃣ Mapear rutas de API y componentes
            app.MapControllers();
            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
