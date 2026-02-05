using System;
using System.Collections.Generic;
using System.Linq;

namespace _12_Music_Streaming_Service
{
    public class MusicManager
    {
        // Storage
        public Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public Dictionary<string, Playlist> Playlists = new Dictionary<string, Playlist>();
        public Dictionary<string, User> Users = new Dictionary<string, User>();

        private int nextSongId = 1;
        private int nextPlaylistId = 1;
        private int nextUserId = 1;

        // Add new user
        public void AddUser(string userName, List<string> favoriteGenres)
        {
            string userId = "U" + nextUserId++;

            Users.Add(userId, new User
            {
                UserId = userId,
                UserName = userName,
                FavoriteGenres = favoriteGenres
            });
        }

        // Add song
        public void AddSong(string title, string artist, string genre,
                            string album, TimeSpan duration)
        {
            string songId = "S" + nextSongId++;

            Songs.Add(songId, new Song
            {
                SongId = songId,
                Title = title,
                Artist = artist,
                Genre = genre,
                Album = album,
                Duration = duration,
                PlayCount = 0
            });
        }

        // Create playlist
        public void CreatePlaylist(string userId, string playlistName)
        {
            if (!Users.ContainsKey(userId))
                return;

            string playlistId = "P" + nextPlaylistId++;

            Playlist playlist = new Playlist
            {
                PlaylistId = playlistId,
                Name = playlistName,
                CreatedBy = userId
            };

            Playlists.Add(playlistId, playlist);
            Users[userId].UserPlaylists.Add(playlist);
        }

        // Add song to playlist
        public bool AddSongToPlaylist(string playlistId, string songId)
        {
            if (!Playlists.ContainsKey(playlistId) || !Songs.ContainsKey(songId))
                return false;

            Playlists[playlistId].Songs.Add(Songs[songId]);
            return true;
        }

        // Group songs by genre
        public Dictionary<string, List<Song>> GroupSongsByGenre()
        {
            return Songs.Values
                        .GroupBy(s => s.Genre)
                        .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Get top played songs
        public List<Song> GetTopPlayedSongs(int count)
        {
            return Songs.Values
                        .OrderByDescending(s => s.PlayCount)
                        .Take(count)
                        .ToList();
        }

        // Simulate playing a song
        public void PlaySong(string songId)
        {
            if (Songs.ContainsKey(songId))
                Songs[songId].PlayCount++;
        }
    }
}
