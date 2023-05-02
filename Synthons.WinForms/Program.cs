using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Synthons.Infrastructure;

namespace Synthons.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddDbContext<SynthonsDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("SynthonsConnectionString"));
            });

            services.AddScoped<Form1>();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
            
        }
    }
}