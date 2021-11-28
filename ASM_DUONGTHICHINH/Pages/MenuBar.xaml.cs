using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class MenuBar : Page
    {
        public MenuBar()
        {
            this.InitializeComponent();
            this.contentFrame.Navigate(typeof(Pages.RegisterPage));
        }
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Debug.WriteLine("Select setting");
            }
            var navigationViewItem = args.SelectedItem as NavigationViewItem;
            switch (navigationViewItem.Tag)
            {
                case "Login":
                    this.contentFrame.Navigate(typeof(Pages.LoginPage));
                    break;
                case "Register":
                    this.contentFrame.Navigate(typeof(Pages.RegisterPage));
                    break;
                case "ListSong":
                    this.contentFrame.Navigate(typeof(Pages.ListSong));
                    break;
                case "CreateSong":
                    this.contentFrame.Navigate(typeof(Pages.CreateSong));
                    break;
                case "Profile":
                    this.contentFrame.Navigate(typeof(Pages.ProfilePages));
                    break;
            }
        }
    }
}
