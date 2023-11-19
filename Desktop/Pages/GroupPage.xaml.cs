using System;
using System.Collections.Generic;
using Application.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using Application.Features.Groups;
using Application.Features.Messages;
using Desktop.Interfaces;
using Desktop.Templates;
using MediatR;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public GroupPage(
            Group group,
            IMediator mediator, 
            ICurrentGroupService currentGroupService, 
            ICurrentUserService currentUserService)
        {
            InitializeComponent();
            Group = group;
            Mediator = mediator;
            CurrentGroupService = currentGroupService;
            CurrentUserService = currentUserService;

            Timer = new DispatcherTimer();
            Timer.Tick += OnTimerTick;
            Timer.Interval = TimeSpan.FromSeconds(1);
            
            DataContext = new
            {
                TemplateSelector = new MessageTemplateSelector(CurrentUserService,
                    (FindResource("MessageSelf") as DataTemplate)!,
                    (FindResource("MessageAnother") as DataTemplate)!)
            };
        }

        private Group Group { get; }
        private DispatcherTimer Timer { get; }
        
        private IMediator Mediator { get; }
        private ICurrentGroupService CurrentGroupService { get; }
        private ICurrentUserService CurrentUserService { get; }

        private void GroupPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (CurrentGroupService.IsSet is false)
                throw new InvalidOperationException("Current Group is not initialized");
            
            Label_GroupName.Content = CurrentGroupService.Group!.Name;
            
            SyncMessages();
            Timer.Start();
        }

        private async void SyncMessages()
        {
            var request = new GetLastMessagesFromGroup.Request
            {
                GroupId = Group.Id,
            };

            var result = await Mediator.Send(request);
            
            result.Switch(
                messages => Control_Messages.ItemsSource = messages,
                notFound => MessageBox.Show("Not Found"));
        }
        
        private void OnTimerTick(object? sender, EventArgs e)
        {
            SyncMessages();
        }

        private async void Button_SendMessage_OnClick(object sender, RoutedEventArgs e)
        {
            var content = TextBox_Message.Text;

            if (content.Length == 0)
            {
                MessageBox.Show("Message cannot be empty");
                return;
            }

            TextBox_Message.Text = string.Empty;

            var request = new CreateMessageAtGroupFromAccount.Request
            {
                AccountId = CurrentUserService.Account!.Id,
                GroupId = Group.Id,
                Content = content,
                SentAt = DateTime.UtcNow
            };

            var result = await Mediator.Send(request);
            
            result.Switch(
                success => { },
                validationFailed => MessageBox.Show("Validation Failed"),
                failed => MessageBox.Show("Validation Failed"));
        }

        private void GroupPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
        }

        private async void Button_AddAccount_OnClick(object sender, RoutedEventArgs e)
        {
            var login = TextBox_UserLoginToAdd.Text;

            var request = new AddAccountToGroupByLogin.Request
            {
                AccountLogin = login,
                GroupId = Group.Id
            };

            var result = await Mediator.Send(request);
            
            result.Switch(
                success => { },
                notFound => MessageBox.Show("Not found"),
                validationFailed => MessageBox.Show("Validation Failed"),
                failed => MessageBox.Show("Failed"));
        }
    }
}
