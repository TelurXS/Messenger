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
using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Features.Accounts;
using Application.Features.Groups;
using Application.Infrastructure.Persistance;
using Desktop.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Desktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMediator mediator, ICurrentUserService currentUserService)
        {
            InitializeComponent();
            Mediator = mediator;
            CurrentUserService = currentUserService;
        }

        private IMediator Mediator { get; }
        private ICurrentUserService CurrentUserService { get; }

        private void MainWindow_OnContentRendered(object? sender, EventArgs e)
        {
            if (CurrentUserService.IsLogined is false)
                throw new ArgumentException("Not logined into account");
            
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
                   //MessageBox.Show(groups.Count.ToString());
                   ListBox_Groups.ItemsSource = groups.ToArray();
               },
               notFound => { });

            //StringBuilder builder = new StringBuilder();
            //var account = AccountService.FindById(6).AsFound;
            //
            //foreach (var group in GroupService.FindAll(x => x.Accounts.Contains(account)).AsFound)
            //{
            //    builder.AppendLine(group.Name);
            //    
            //    foreach (var a in group.Accounts )
            //    {
            //        builder.AppendLine("\t" + a.Name);
            //    }
            //
            //    builder.AppendLine();
            //}
            //
            //MessageBox.Show(builder.ToString());
        }

        private async void Button_RefreshGroups_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateGroupList();
        }

        private async void Button_Add_OnClick(object sender, RoutedEventArgs e)
        {
            var request = new AddAccountToGroup.Request
            {
                AccountId = CurrentUserService.Account!.Id,
                GroupId = 2,
            };
            
            var result = await Mediator.Send(request);
            
            result.Switch(
                x => MessageBox.Show("Success"),
                x => MessageBox.Show("NotFound"),
                x => MessageBox.Show("Val Er"),
                x => MessageBox.Show("Failure"));
        }

        private async void Button_Remove_OnClick(object sender, RoutedEventArgs e)
        {
            var request = new RemoveAccountFromGroup.Request
            {
                AccountId = CurrentUserService.Account!.Id,
                GroupId = 2,
            };
            
            var result = await Mediator.Send(request);
            
            result.Switch(
                x => MessageBox.Show("Success"),
                x => MessageBox.Show("NotFound"),
                x => MessageBox.Show("Val Er"),
                x => MessageBox.Show("Failure"));
        }
    }
}
