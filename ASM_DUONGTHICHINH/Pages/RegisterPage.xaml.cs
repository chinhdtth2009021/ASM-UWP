using ASM_DUONGTHICHINH.Entities;
using ASM_DUONGTHICHINH.Services;
using DocuSign.eSign.Model;
using Microsoft.Graph;
using Microsoft.Web.Mvc.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;
using static com.sun.jndi.cosnaming.IiopUrl;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM_DUONGTHICHINH.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private AccountService accountService = new AccountService();
        private FileService fileService = new FileService();
        private int choosedGender = 1;
        private string _avatarUrl;
        public RegisterPage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(880, 800));
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var account = new Account()
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                password = Password.Password.ToString(),
                avatar = _avatarUrl,
                email = Email.Text,
                phone = Phone.Text,
                address = Address.Text,
                introduction = Introduction.Text,
                gender = choosedGender,
                birthday = DatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"),
            };
            var result = await accountService.RegisterAsyn(account);
            if (result)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Register success. Your email is {account.email}";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.LoginPages));
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Register fails. Please try again!";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var currentTag = radioButton.Tag;
            switch (currentTag)
            {
                case "Male":
                    choosedGender = 1;
                    break;
                case "Female":
                    choosedGender = 0;
                    break;
                case "Other":
                    choosedGender = 2;
                    break;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.LoginPages));
        }

        private async void Handle_Camera(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                ProcessRing.IsActive = true;
                // User cancelled photo capture
                var result = await fileService.UploadFile(photo);
                if (result != null)
                {
                    _avatarUrl = result;
                    PreviewPhoto.ImageSource = new BitmapImage(new Uri(result));
                    PreviewPhoto.Stretch = Stretch.UniformToFill;
                }
                ProcessRing.IsActive = false;
            }
        }

        private async void Handle_Upload_Image(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            StorageFile choseFile = await filePicker.PickSingleFileAsync();
            if (choseFile != null)
            {
                ProcessRing.IsActive = true;
                var result = await fileService.UploadFile(choseFile);
                _avatarUrl = result;
                PreviewPhoto.ImageSource = new BitmapImage(new Uri(result));
                PreviewPhoto.Stretch = Stretch.UniformToFill;
            }
            ProcessRing.IsActive = false;
        }
    } 
}
