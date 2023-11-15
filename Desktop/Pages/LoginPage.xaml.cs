using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.Common.Models.Results;
using Application.Entities;
using Application.Features.Accounts;
using Desktop.Interfaces;
using MediatR;

namespace Desktop.Pages;

public partial class LoginPage : Page
{
    public LoginPage(IMediator mediator, ICurrentUserService currentUserService)
    {
        InitializeComponent();
        Mediator = mediator;
        CurrentUserService = currentUserService;
    }

    public event Action<Account>? OnLogin;
    
    private IMediator Mediator { get; }
    private ICurrentUserService CurrentUserService { get; }

    private async void Button_Login_OnClick(object sender, RoutedEventArgs e)
    {
        var request = new LoginIntoAccount.Request
        {
            Login = TextBox_Login.Text,
            Password= TextBox_Password.Text,
        };

        var result = await Mediator.Send(request);
        
        result.Switch(
            OnSuccess,
            OnNotFound,
            OnValidationFailed,
            OnFailed);
    }

    private void OnSuccess(Account account)
    {
        Label_Result.Foreground = FindResource("SuccessBrush") as Brush;
        Label_Result.Text = "Success";
        
        CurrentUserService.Login(account);
        
        OnLogin?.Invoke(account);
    }

    private void OnNotFound(NotFound notFound)
    {
        Label_Result.Foreground = FindResource("ErrorBrush") as Brush;
        Label_Result.Text = "Account with this login is not exist";
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