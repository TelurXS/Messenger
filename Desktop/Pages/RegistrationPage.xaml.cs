using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.Common.Models.Results;
using Application.Entities;
using Application.Features.Accounts;
using MediatR;

namespace Desktop.Pages;

public partial class RegistrationPage : Page
{
    public RegistrationPage(IMediator mediator)
    {
        InitializeComponent();
        Mediator = mediator;
    }
    
    private IMediator Mediator { get; }

    private async void Button_Register_OnClick(object sender, RoutedEventArgs e)
    {
        var request = new CreateAccount.Request
        {
            Login = TextBox_Login.Text,
            Name = TextBox_Name.Text,
            Email = TextBox_Email.Text,
            Password = TextBox_Password.Text
        };

        var result = await Mediator.Send(request);
        
        result.Switch(
            OnSuccess,
            OnValidationFailed,
            OnFailed);
    }
    
    private void OnSuccess(Account account)
    {
        Label_Result.Foreground = FindResource("SuccessBrush") as Brush;
        Label_Result.Text = "Created";
    }

    private void OnValidationFailed(ValidationFailed validationFailed)
    {
        Label_Result.Foreground = FindResource("ErrorBrush") as Brush;
        Label_Result.Text = string.Empty;

        foreach (var error in validationFailed.Errors)
        {
            Label_Result.Text += $"{error.ErrorMessage}\n";
        }
    }
    
    private void OnFailed(Failed failed)
    {
        Label_Result.Foreground = FindResource("ErrorBrush") as Brush;
        Label_Result.Text = "Failed";
    }
}