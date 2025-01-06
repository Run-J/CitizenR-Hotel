using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotelAppLibrary.Data;
using HotelAppLibrary.Databases;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Entry point for the WPF application. 
    /// Configures Dependency Injection (DI) and application services.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Overrides the startup behavior of the application.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // DI
            // Create ServiceCollection
            var services = new ServiceCollection(); // "ServiceCollection" is a container for registering dependencies and services in the Dependency Injection (DI) system.

            // Register services (Dependency Injection)
            services.AddTransient<MainWindow>(); // Lifetimes of Transient: A new instance every time it’s requested.	
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IDatabaseData, SqlData>();



            // Configuration(json)
            // Load Configuration from appsettings.json
            var builder = new ConfigurationBuilder() // "ConfigurationBuilder" is a tool for loading configuration settings into our application
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();

            // Register the configuration as a singleton service
            services.AddSingleton(config); // Lifetimes of Singleton: One instance for the entire app lifetime.



            // Service Provider
            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Resolve MainWindow from the service provider;
            // Ensures that all dependencies of MainWindow (like database connections or services)
            // are automatically injected when MainWindow is created.
            var mainWindow = serviceProvider.GetService<MainWindow>(); // "serviceProvider.GetService<T>()" retrieves a registered dependency from the DI container.
            if (mainWindow != null)
            {
                // Show the main window
                mainWindow.Show();
            }
            else
            {
                throw new InvalidOperationException("Failed to resolve MainWindow from ServiceProvider.");
            }

        }
    }
}
