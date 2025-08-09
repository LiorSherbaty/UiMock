using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using UiTesterDemo.Models;
using UiTesterDemo.Services;
using UiTesterDemo.ViewModels;

namespace UiTesterDemo
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            // Create a host builder, the modern way to configure .NET apps
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Add configuration options
                    services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));

                    // Register the services and viewmodels
                    ConfigureServices(services, context);
                });

            _host = hostBuilder.Build();
        }

        private void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            // Retrieve app settings to decide which service to register
            var appSettings = context.Configuration.GetSection("AppSettings").Get<AppSettings>();

            // --- This is the key part for switching between mock and real services ---
            if (appSettings?.MockEnabled == true)
            {
                // In mock mode, we register MockWeatherService as a singleton
                // so we can control its state from the UI tester.
                var mockService = new MockWeatherService();
                services.AddSingleton<IWeatherService>(mockService);
                services.AddSingleton<IMockManager>(mockService); // Register the mock manager interface
            }
            else
            {
                // In real mode, we register the API service.
                // We add HttpClient and configure its base address.
                services.AddHttpClient<IWeatherService, WeatherApiService>(client =>
                {
                    client.BaseAddress = new Uri(appSettings?.ApiBaseUrl ?? "");
                });
            }

            // Register the main window and its viewmodel
            services.AddSingleton<MainViewModel>();
            services.AddSingleton(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5)); // Gracefully shutdown
            }

            base.OnExit(e);
        }
    }
}
