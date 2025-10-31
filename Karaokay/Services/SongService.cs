using System;
using System.Collections.Generic;
using System.Linq;
using Karaokay.Models;

namespace Karaokay.Services
{
    public class SongService
    {
        private static SongService? _instance;
        private static readonly object _lock = new object();
        private List<Song> _songs;

        private SongService()
        {
            _songs = new List<Song>();
            LoadSampleData();
        }

        public static SongService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SongService();
                        }
                    }
                }
                return _instance;
            }
        }

        private void LoadSampleData()
        {
            // Sample data for demonstration
            _songs = new List<Song>
            {
                new Song
                {
                    Id = 1,
                    Title = "Bohemian Rhapsody",
                    Artist = "Queen",
                    Duration = "5:55",
                    Genre = "Rock",
                    IsFavorite = true,
                    PlayCount = 10
                },
                new Song
                {
                    Id = 2,
                    Title = "Imagine",
                    Artist = "John Lennon",
                    Duration = "3:03",
                    Genre = "Pop",
                    IsFavorite = false,
                    PlayCount = 5
                },
                new Song
                {
                    Id = 3,
                    Title = "Billie Jean",
                    Artist = "Michael Jackson",
                    Duration = "4:54",
                    Genre = "Pop",
                    IsFavorite = true,
                    PlayCount = 15
                },
                new Song
                {
                    Id = 4,
                    Title = "Hotel California",
                    Artist = "Eagles",
                    Duration = "6:30",
                    Genre = "Rock",
                    IsFavorite = false,
                    PlayCount = 8
                },
                new Song
                {
                    Id = 5,
                    Title = "Sweet Child O' Mine",
                    Artist = "Guns N' Roses",
                    Duration = "5:56",
                    Genre = "Rock",
                    IsFavorite = true,
                    PlayCount = 12
                },
                new Song
                {
                    Id = 6,
                    Title = "Smells Like Teen Spirit",
                    Artist = "Nirvana",
                    Duration = "5:01",
                    Genre = "Grunge",
                    IsFavorite = false,
                    PlayCount = 7
                }
            };
        }

        public List<Song> GetAllSongs()
        {
            return _songs;
        }

        public List<Song> GetFavorites()
        {
            return _songs.Where(s => s.IsFavorite).ToList();
        }

        public List<Song> GetRecent()
        {
            return _songs.OrderByDescending(s => s.DateAdded).Take(10).ToList();
        }

        public List<Song> GetUserLibrary()
        {
            return _songs.Where(s => s.IsUserAdded).OrderByDescending(s => s.DateAdded).ToList();
        }

        public List<Song> SearchSongs(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _songs;

            query = query.ToLower();
            return _songs.Where(s => 
                s.Title.ToLower().Contains(query) || 
                s.Artist.ToLower().Contains(query) ||
                s.Genre.ToLower().Contains(query)
            ).ToList();
        }

        public void AddSong(Song song)
        {
            song.Id = _songs.Any() ? _songs.Max(s => s.Id) + 1 : 1;
            song.DateAdded = DateTime.Now;
            _songs.Add(song);
        }

        public void ToggleFavorite(int songId)
        {
            var song = _songs.FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                song.IsFavorite = !song.IsFavorite;
            }
        }

        public void IncrementPlayCount(int songId)
        {
            var song = _songs.FirstOrDefault(s => s.Id == songId);
            if (song != null)
            {
                song.PlayCount++;
            }
        }
    }
}
