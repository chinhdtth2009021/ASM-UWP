using ASM_DUONGTHICHINH.Entities;
using ASM_DUONGTHICHINH.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class ListSong : Page
    {
        private MusicService songService = new MusicService();
        public ListSong()
        {
            this.InitializeComponent();
            this.Loaded += ListSong_LoadedAsync;
        }
        private async void ListSong_LoadedAsync(object sender, RoutedEventArgs e)
        {
            var listSong = await songService.GetLatestSongAsync();
            MyListView.ItemsSource = listSong;
        }

        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = (Music)MyListView.SelectedItem;
            MyMediaPlayer.Source = MediaSource.CreateFromUri(new Uri(selectedItem.link));
        }

    }
}
