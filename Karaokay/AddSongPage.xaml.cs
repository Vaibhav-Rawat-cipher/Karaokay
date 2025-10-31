using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Karaokay.Models;
using Karaokay.Services;

namespace Karaokay
{
    public partial class AddSongPage : Window
    {
        private readonly SongService _songService;
        private string? _selectedFilePath;

        public AddSongPage()
        {
            InitializeComponent();
            _songService = new SongService();

            // Enable window dragging
            this.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    try
                    {
                        this.DragMove();
                    }
                    catch { }
                }
            };
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Audio File",
                Filter = "Audio Files (*.mp3;*.wav;*.m4a;*.flac;*.ogg)|*.mp3;*.wav;*.m4a;*.flac;*.ogg|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;
                SelectedFileText.Text = Path.GetFileName(_selectedFilePath);
                SelectedFileText.Foreground = System.Windows.Media.Brushes.White;
            }
        }

        private void UploadArea_Click(object sender, MouseButtonEventArgs e)
        {
            BrowseButton_Click(sender, e);
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Please enter a song title.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ArtistTextBox.Text))
            {
                MessageBox.Show("Please enter an artist name.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_selectedFilePath) && string.IsNullOrWhiteSpace(YouTubeUrlTextBox.Text))
            {
                MessageBox.Show("Please select an audio file or provide a YouTube URL.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Show progress
            GenerateButton.IsEnabled = false;
            ProgressPanel.Visibility = Visibility.Visible;
            ProgressText.Text = "Processing your song...";

            try
            {
                // Simulate processing (replace with actual karaoke generation logic)
                await System.Threading.Tasks.Task.Delay(2000);

                // Get selected genre
                string genre = "Unknown";
                if (GenreComboBox.SelectedItem is System.Windows.Controls.ComboBoxItem selectedItem)
                {
                    genre = selectedItem.Content.ToString() ?? "Unknown";
                }

                // Create new song
                var newSong = new Song
                {
                    Title = TitleTextBox.Text.Trim(),
                    Artist = ArtistTextBox.Text.Trim(),
                    Genre = genre,
                    AudioFilePath = _selectedFilePath ?? string.Empty,
                    DateAdded = DateTime.Now,
                    Duration = "0:00", // TODO: Calculate actual duration
                    ThumbnailUrl = string.Empty, // TODO: Generate or fetch thumbnail
                    IsFavorite = false,
                    PlayCount = 0
                };

                // Add song to service
                _songService.AddSong(newSong);

                ProgressText.Text = "Karaoke generated successfully!";
                await System.Threading.Tasks.Task.Delay(1000);

                MessageBox.Show($"Successfully added '{newSong.Title}' by {newSong.Artist}!\n\n" +
                              "The karaoke track is now available in your library.",
                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close window and return to main window
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the song:\n{ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
                GenerateButton.IsEnabled = true;
                ProgressPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
