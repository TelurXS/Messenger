using System.Windows;
using System.Windows.Controls;
using Desktop.Interfaces;

namespace Desktop.Pages;

public partial class DefaultPage : Page
{
    public DefaultPage(ICurrentUserService currentUserService)
    {
        CurrentUserService = currentUserService;
        InitializeComponent();
    }
    
    private ICurrentUserService CurrentUserService { get; }

    private void DefaultPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        Label_Username.Content = CurrentUserService.Account!.Name;
    }
}