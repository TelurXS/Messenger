using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Application.Common.Extensions;
using Desktop.Extensions;
using Desktop.Pages;
using Desktop.Windows;
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
            Services.AddTransient<MainWindow>();

            Services.AddTransient<GroupPage>();
            Services.AddTransient<ProfilePage>();
            
            Services.AddPages(x => new List<Page>()
            {
                x.GetRequiredService<GroupPage>(),
                x.GetRequiredService<ProfilePage>()
            });

            Provider = Services.BuildServiceProvider();

            var bindings = Provider.GetRequiredService<PageBindings>();

            foreach (var page in bindings)
            {
                MessageBox.Show(page.GetType().Name);
            }
        }
    }
}