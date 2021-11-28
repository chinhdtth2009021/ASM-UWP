using ASM_DUONGTHICHINH.Entities;
using ASM_DUONGTHICHINH.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM_DUONGTHICHINH.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        AccountService accountService = new AccountService();
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginInfomation = new LoginInfomation()
            {
                email = txtEmail.Text,
                password = txtPassword.Password.ToString()
            };
            var credential = await accountService.loginAsync(loginInfomation);
            if (credential == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Please try again later!";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action sucsses";
                contentDialog.Content = $"Welcome back";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.RegisterPage));
        }
    }
}
