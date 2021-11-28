using ASM_DUONGTHICHINH.Services;
using Stripe;
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
using AccountService = ASM_DUONGTHICHINH.Services.AccountService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM_DUONGTHICHINH.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePages : Page
    {
        public ProfilePages()
        {
            this.InitializeComponent();
            private AccountService accountService = new AccountService();
    }
    private async void ProfilePages_LoadedAsync(object sender, RoutedEventArgs e)
    {
        var account = await AccountService.GetInfomation();
        if (account == null)
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.Title = "Login required";
            contentDialog.Content = $"Please login to continue";
            contentDialog.PrimaryButtonText = "Got it";
            this.Frame.Navigate(typeof(LoginPage));
        }
        else
        {
            Email.Text = account.email;
            FullName.Text = account.firstName + " " + account.lastName;
        }
    }
}

