using System;
using System.Linq;
using Application.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Application.Features.Accounts;
using Application.Features.Groups;
using Desktop.Interfaces;
using Desktop.Pages;
using MediatR;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(
            IMediator mediator, 
            ICurrentUserService currentUserService, 
            ICurrentGroupService currentGroupService,
            IGroupPageFactory groupPageFactory,
            DefaultPage defaultPage,
            ProfilePage profilePage,
            CreateGroupPage createGroupPage)
        {
            InitializeComponent();
            Mediator = mediator;
            CurrentUserService = currentUserService;
            CurrentGroupService = currentGroupService;
            GroupPageFactory = groupPageFactory;
            DefaultPage = defaultPage;
            ProfilePage = profilePage;
            CreateGroupPage = createGroupPage;
        }

        private IMediator Mediator { get; }
        private ICurrentUserService CurrentUserService { get; }
        private ICurrentGroupService CurrentGroupService { get; }
        private IGroupPageFactory GroupPageFactory { get; }
        private DefaultPage DefaultPage { get; }
        private ProfilePage ProfilePage { get; }
        private CreateGroupPage CreateGroupPage { get; }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Frame_Page.Navigate(DefaultPage);
            
            if (CurrentUserService.IsLogined is false)
                throw new InvalidOperationException("Not logined into account");
            
            var account = CurrentUserService.Account!;

            Label_AccountName.Content = account.Name;
            UpdateGroupList();
        }

        private async void UpdateGroupList()
        {
           var request = new GetAllGroupsFromAccount.Request
           {
               Id = CurrentUserService.Account!.Id
           };
           
           var result = await Mediator.Send(request);
           
           result.Switch(
               groups =>
               {
                   ListBox_Groups.ItemsSource = groups.ToList();
               },
               notFound => { });
        }

        private async void Button_RefreshGroups_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateGroupList();
        }

        private void Profile_OnClick(object sender, MouseButtonEventArgs e)
        {
            Frame_Page.Navigate(ProfilePage);
        }

        private void ListBox_Groups_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ListBox_Groups.SelectedItem as Group;
            
            if (selected is null)
                return;
            
            CurrentGroupService.Set(selected);
            Frame_Page.Navigate(GroupPageFactory.Create(selected));
        }

        private void Button_CreateGroup_OnClick(object sender, RoutedEventArgs e)
        {
            Frame_Page.Navigate(CreateGroupPage);
        }

        private async void MenuItem_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var group = ListBox_Groups.SelectedItem as Group;

            if (group is null)
            {
                MessageBox.Show("Not selected");
                return;
            }

            var request = new DeleteGroupById.Request
            {
                Id = group.Id
            };

            var result = await Mediator.Send(request);
            
            result.Switch(
                success =>
                {
                    UpdateGroupList();
                    Frame_Page.Navigate(DefaultPage);
                },
                notFound => MessageBox.Show("Not found"),
                failed => MessageBox.Show("Failed"));
        }
    }
}
