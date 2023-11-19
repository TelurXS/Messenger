using System;
using System.Windows;
using System.Windows.Controls;
using Application.Entities;
using Desktop.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Desktop.Templates;

public class MessageTemplateSelector : DataTemplateSelector
{
    public MessageTemplateSelector(ICurrentUserService currentUserService, DataTemplate messageSelf, DataTemplate messageAnother)
    {
        CurrentUserService = currentUserService;
        MessageSelf = messageSelf;
        MessageAnother = messageAnother;
    }
    
    private ICurrentUserService CurrentUserService { get; }
    private DataTemplate MessageSelf { get; }
    private DataTemplate MessageAnother { get; }

    public override DataTemplate SelectTemplate(object? item, DependencyObject container)
    {
        var account = CurrentUserService.Account!;

        if (item is not Message message)
            throw new ArgumentException();

        if (message.Sender is null)
            return MessageAnother;

        return message.Sender.Id == account.Id 
            ? MessageSelf 
            : MessageAnother;
    }
}