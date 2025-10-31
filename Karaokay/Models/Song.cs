using System;

namespace Karaokay.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Duration { get; set; } = "0:00";
        public string Genre { get; set; } = "Unknown";
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string AudioFilePath { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool IsFavorite { get; set; }
        public int PlayCount { get; set; }
    }
}
