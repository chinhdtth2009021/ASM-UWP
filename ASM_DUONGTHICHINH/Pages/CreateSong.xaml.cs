using ASM_DUONGTHICHINH.Entities;
using ASM_DUONGTHICHINH.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TechTalk.SpecFlow.Analytics.UserId;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FileService = ASM_DUONGTHICHINH.Services.FileService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM_DUONGTHICHINH.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
       
        private FileService fileService;
        private MusicService musicService;
        public CreateSong()
        {
            this.InitializeComponent();
            this.Loaded += CreateSong_Loaded; ;
        }

        private void CreateSong_Loaded(object sender, RoutedEventArgs e)
        {
            fileService = new FileService();
            musicService = new MusicService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var music = new Music()
            {
                name = Name.Text,
                author = Author.Text,
                singer = Singer.Text,
                thumbnail = Thumbnail.Text,
                description = Description.Text,
                link = Link.Text,
                message = Message.Text
            };
            var result = await musicService.CreateSongAsync(music);
            if (result != null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Upload song success.";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
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

        private async void Handle_Click_UploadImage(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                // config picker.
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var result = await fileService.UploadFile(file);
                Thumbnail.Text = result;
            }
        }

        private async void Handle_Click_UploadMp3(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                // config picker.
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".mp3");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var result = await fileService.UploadFile(file);
                Link.Text = result;
            }
        }


    }
}
