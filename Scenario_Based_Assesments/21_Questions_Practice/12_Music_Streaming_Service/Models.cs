using System;
using System.Collections.Generic;

namespace _12_Music_Streaming_Service
{
    // Represents a single song
    public class Song
    {
        public string SongId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public int PlayCount { get; set; }
    }

    // Represents a playlist
    public class Playlist
    {
        public string PlaylistId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
    }

    // Represents a user
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> FavoriteGenres { get; set; } = new List<string>();
        public List<Playlist> UserPlaylists { get; set; } = new List<Playlist>();
    }
}
