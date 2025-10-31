using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Karaokay.Models;
using Karaokay.Services;

namespace Karaokay
{
    public partial class MainWindow : Window
    {
        private readonly SongService _songService;
        private Song? _currentSong;
        private string _currentFilter = "All";

        public MainWindow()
        {
            InitializeComponent();
            _songService = new SongService();
            LoadSongs();
            
            // Enable window dragging
            this.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            };
        }

        private void LoadSongs(string filter = "All")
        {
            List<Song> songs = filter switch
            {
                "Favorites" => _songService.GetFavorites(),
                "Recent" => _songService.GetRecent(),
                _ => _songService.GetAllSongs()
            };

            SongListView.ItemsSource = songs;

            // Select first song if available
            if (songs.Any() && _currentSong == null)
            {
                DisplaySongDetails(songs.First());
            }
        }

        private void DisplaySongDetails(Song song)
        {
            _currentSong = song;
            DetailTitle.Text = song.Title;
            DetailArtist.Text = song.Artist;
            DetailDuration.Text = song.Duration;
            DetailGenre.Text = song.Genre;

            // Load thumbnail if available
            if (!string.IsNullOrEmpty(song.ThumbnailUrl))
            {
                try
                {
                    DetailThumbnail.Source = new BitmapImage(new Uri(song.ThumbnailUrl));
                }
                catch
                {
                    DetailThumbnail.Source = null;
                }
            }
            else
            {
                DetailThumbnail.Source = null;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchQuery = SearchBox.Text;
            var filteredSongs = _songService.SearchSongs(searchQuery);

            // Apply current filter
            if (_currentFilter == "Favorites")
            {
                filteredSongs = filteredSongs.Where(s => s.IsFavorite).ToList();
            }
            else if (_currentFilter == "Recent")
            {
                filteredSongs = filteredSongs.OrderByDescending(s => s.DateAdded).Take(10).ToList();
            }

            SongListView.ItemsSource = filteredSongs;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string filter)
            {
                _currentFilter = filter;
                LoadSongs(filter);
                
                // Update search if there's text
                if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    SearchBox_TextChanged(SearchBox, new TextChangedEventArgs(TextBox.TextChangedEvent, UndoAction.None));
                }
            }
        }

        private void SongItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Tag is Song song)
            {
                DisplaySongDetails(song);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSong == null)
            {
                MessageBox.Show("Please select a song first.", "No Song Selected", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // TODO: Implement actual audio playback
            // For now, just show a message
            MessageBox.Show($"Playing: {_currentSong.Title} by {_currentSong.Artist}\n\n" +
                          "Audio playback will be implemented when you integrate your karaoke generation model.",
                          "Karaoke Playback", MessageBoxButton.OK, MessageBoxImage.Information);

            _songService.IncrementPlayCount(_currentSong.Id);
        }

        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // TODO: Implement seek functionality when audio playback is added
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Open Add Song page
            var addSongPage = new AddSongPage();
            addSongPage.Owner = this;
            
            if (addSongPage.ShowDialog() == true)
            {
                // Refresh the song list after adding a new song
                LoadSongs(_currentFilter);
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized 
                ? WindowState.Normal 
                : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
