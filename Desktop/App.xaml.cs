using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Application.Common.Extensions;
using Application.Common.Interfaces.Persistance;
using Application.Infrastructure.Persistance;
using Desktop.Extensions;
using Desktop.Infrastructure.Services;
using Desktop.Interfaces;
using Desktop.Pages;
using Desktop.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
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

        private IServiceProvider Provider { get; set; } = null!;
        private IServiceCollection Services { get; }
        private IConfiguration Configuration { get; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureServices();

            var window = Provider.GetRequiredService<StartupWindow>();
            window.Show();
        }

        private void ConfigureServices()
        {
            Services.AddSingleton(Configuration);
            
            Services.AddApplication();
            Services.AddInfrastructure(Configuration);
            
            Services.AddWindowsFromAssemblyContaining<AssemblyInfo>();
            Services.AddPagesFromAssemblyContaining<AssemblyInfo>();

            Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            Provider = Services.BuildServiceProvider();
        }
    }
}