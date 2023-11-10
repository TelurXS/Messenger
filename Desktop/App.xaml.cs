using System;
using System.Windows;
using Application.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using DesktopApplication = System.Windows.Application;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : DesktopApplication
    {
        public App()
        {
            Services =  new ServiceCollection();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        
        private IServiceProvider Provider { get; set; }
        private IServiceCollection Services { get; }
        private IConfiguration Configuration { get; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureServices();

            var window = Provider.GetRequiredService<LoginWindow>();
            window.Show();
        }

        private void ConfigureServices()
        {
            Services.AddSingleton(Configuration);
            
            Services.AddApplication();
            Services.AddInfrastructure(Configuration);

            Services.AddTransient<LoginWindow>();

            Provider = Services.BuildServiceProvider();
        }
    }
}