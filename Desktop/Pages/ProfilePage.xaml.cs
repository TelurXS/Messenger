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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application.Common.Models.Messages;
using Application.Common.Models.Results;
using Application.Entities;
using Application.Features.Accounts;
using Desktop.Interfaces;
using MediatR;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage(
            ICurrentUserService currentUserService,
            IMediator mediator)
        {
            InitializeComponent();
            CurrentUserService = currentUserService;
            Mediator = mediator;
        }
        
        private ICurrentUserService CurrentUserService { get; }
        private IMediator Mediator { get; }

        private void FillFields()
        {
            var account = CurrentUserService.Account!;

            TextBox_Name.Text = account.Name;
            TextBox_Login.Text = account.Login;
            TextBox_Email.Text = account.Email;
            TextBox_Password.Text = account.Password;
        }

        private void ProfilePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            FillFields();
        }

        private void Button_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            FillFields();
        }

        private async void Button_Save_OnClick(object sender, RoutedEventArgs e)
        {
            var id = CurrentUserService.Account!.Id;
            var name = TextBox_Name.Text;
            var login = TextBox_Login.Text;
            var email = TextBox_Email.Text;
            var password = TextBox_Password.Text;

            var request = new UpdateAccount.Request
            {
                Id = id,
                Email = email,
                Login = login,
                Name = name,
                Password = password
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
    }
}
