using System;
using System.Collections.Generic;
using System.Linq;

namespace _12_Music_Streaming_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicManager manager = new MusicManager();

            // ✅ Hardcoded Users
            manager.AddUser("Asad", new List<string> { "Pop", "Rock" });
            manager.AddUser("Rohit", new List<string> { "Hip-Hop", "Jazz" });

            // ✅ Hardcoded Songs
            manager.AddSong("Blinding Lights", "The Weeknd", "Pop", "After Hours", TimeSpan.FromMinutes(3.2));
            manager.AddSong("Shape of You", "Ed Sheeran", "Pop", "Divide", TimeSpan.FromMinutes(4.1));
            manager.AddSong("Believer", "Imagine Dragons", "Rock", "Evolve", TimeSpan.FromMinutes(3.4));
            manager.AddSong("Lose Yourself", "Eminem", "Hip-Hop", "8 Mile", TimeSpan.FromMinutes(5.2));
            manager.AddSong("Take Five", "Dave Brubeck", "Jazz", "Time Out", TimeSpan.FromMinutes(5.5));

            // Simulate plays
            manager.PlaySong("S1");
            manager.PlaySong("S1");
            manager.PlaySong("S3");

            while (true)
            {
                Console.WriteLine("\n=== Music Streaming Service ===");
                Console.WriteLine("1. View All Songs");
                Console.WriteLine("2. Group Songs By Genre");
                Console.WriteLine("3. Create Playlist");
                Console.WriteLine("4. Add Song To Playlist");
                Console.WriteLine("5. View Top Played Songs");
                Console.WriteLine("6. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n--- All Songs ---");

                    foreach (var song in manager.Songs.Values)
                    {
                        Console.WriteLine($"{song.SongId} | {song.Title} | {song.Artist} | {song.Genre} | Plays: {song.PlayCount}");
                    }
                }

                else if (choice == "2")
                {
                    Console.WriteLine("\n--- Songs By Genre ---");

                    var grouped = manager.GroupSongsByGenre();

                    foreach (var genre in grouped)
                    {
                        Console.WriteLine($"\n{genre.Key}:");

                        foreach (var song in genre.Value)
                            Console.WriteLine($"  {song.Title} - {song.Artist}");
                    }
                }

                else if (choice == "3")
                {
                    Console.Write("Enter User ID (U1, U2...): ");
                    string userId = Console.ReadLine();

                    Console.Write("Playlist Name: ");
                    string name = Console.ReadLine();

                    manager.CreatePlaylist(userId, name);
                    Console.WriteLine("Playlist created!");
                }

                else if (choice == "4")
                {
                    Console.Write("Playlist ID: ");
                    string playlistId = Console.ReadLine();

                    Console.Write("Song ID: ");
                    string songId = Console.ReadLine();

                    if (manager.AddSongToPlaylist(playlistId, songId))
                        Console.WriteLine("Song added!");
                    else
                        Console.WriteLine("Failed to add song.");
                }

                else if (choice == "5")
                {
                    Console.Write("How many top songs?: ");
                    int count = int.Parse(Console.ReadLine());

                    var topSongs = manager.GetTopPlayedSongs(count);

                    Console.WriteLine("\n--- Top Played Songs ---");

                    foreach (var song in topSongs)
                        Console.WriteLine($"{song.Title} | Plays: {song.PlayCount}");
                }

                else if (choice == "6")
                {
                    Console.WriteLine("Thank you!");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}
