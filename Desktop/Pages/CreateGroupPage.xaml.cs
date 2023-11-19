using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.Common.Models.Messages;
using Application.Common.Models.Results;
using Application.Entities;
using Application.Features.Groups;
using Desktop.Interfaces;
using MediatR;

namespace Desktop.Pages;

public partial class CreateGroupPage : Page
{
    public CreateGroupPage(IMediator mediator, ICurrentUserService currentUserService)
    {
        InitializeComponent();
        Mediator = mediator;
        CurrentUserService = currentUserService;
    }
    
    private IMediator Mediator { get; }
    private ICurrentUserService CurrentUserService { get; }


    private async void Button_Save_OnClick(object sender, RoutedEventArgs e)
    {
        var request = new CreateGroup.Request
        {
            Name = TextBox_Name.Text,
            Description = TextBox_Description.Text
        };

        var result = await Mediator.Send(request);
        
        result.Switch(OnSuccess,
            OnValidationFailed,
            OnFailed);
    }

    private async void OnSuccess(Group group)
    {
        Label_Result.Foreground = FindResource("SuccessBrush") as Brush;
        Label_Result.Text = "Success";

        var request = new AddAccountToGroup.Request
        {
            AccountId = CurrentUserService.Account!.Id,
            GroupId = group.Id
        };

        var result = await Mediator.Send(request);
        
        result.Switch(
            success => { },
            OnNotFound,
            OnValidationFailed,
            OnFailed);
    }

    private void OnNotFound(NotFound notFound)
    {
        Label_Result.Foreground = FindResource("ErrorBrush") as Brush;
        Label_Result.Text = TranslatableMessages.Validation.Accounts.ID_IS_NOT_EXIST;
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

    private void Button_Cancel_OnClick(object sender, RoutedEventArgs e)
    {
        TextBox_Name.Text = string.Empty;
        TextBox_Description.Text = string.Empty;
    }
}