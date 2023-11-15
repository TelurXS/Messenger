using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application.Entities;
using Desktop.Pages;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow(
            MainWindow mainWindow, 
            LoginPage loginPage, 
            RegistrationPage registrationPage)
        {
            InitializeComponent();
            MainWindow = mainWindow;
            LoginPage = loginPage;
            RegistrationPage = registrationPage;

            LoginPage.OnLogin += OnLogin;
        }

        private MainWindow MainWindow { get; }
        private LoginPage LoginPage { get; }
        private RegistrationPage RegistrationPage { get; }

        private void StartupWindow_OnContentRendered(object? sender, EventArgs e)
        {
            Frame_Page.Navigate(LoginPage);
        }

        private void Button_LoginPage_OnClick(object sender, RoutedEventArgs e)
        {
            Frame_Page.Navigate(LoginPage);
        }

        private void Button_RegistrationPage_OnClick(object sender, RoutedEventArgs e)
        {
            Frame_Page.Navigate(RegistrationPage);
        }
        
        private void OnLogin(Account account)
        {
            Close();
            MainWindow.Show();
        }
    }
}
