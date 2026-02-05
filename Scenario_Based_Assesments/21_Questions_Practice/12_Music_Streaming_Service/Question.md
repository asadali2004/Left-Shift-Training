# Question 12: Music Streaming Service

## Scenario
A music platform needs to manage songs, playlists, and user preferences.

## Requirements

### In class Song:
- `string SongId`
- `string Title`
- `string Artist`
- `string Genre`
- `string Album`
- `TimeSpan Duration`
- `int PlayCount`

### In class Playlist:
- `string PlaylistId`
- `string Name`
- `string CreatedBy`
- `List<Song> Songs`

### In class User:
- `string UserId`
- `string UserName`
- `List<string> FavoriteGenres`
- `List<Playlist> UserPlaylists`

### In class MusicManager:

#### Method 1
```csharp
public void AddSong(string title, string artist, string genre, string album, TimeSpan duration)
```

#### Method 2
```csharp
public void CreatePlaylist(string userId, string playlistName)
```

#### Method 3
```csharp
public bool AddSongToPlaylist(string playlistId, string songId)
```

#### Method 4
```csharp
public Dictionary<string, List<Song>> GroupSongsByGenre()
```

#### Method 5
```csharp
public List<Song> GetTopPlayedSongs(int count)
```

## Sample Use Cases:
1. Add songs with metadata
2. Create and manage playlists
3. Group songs by genre
4. Track song popularity
5. User profile management
csharp
// In class Song:
// - string SongId
// - string Title
// - string Artist
// - string Genre
// - string Album
// - TimeSpan Duration
// - int PlayCount

// In class Playlist:
// - string PlaylistId
// - string Name
// - string CreatedBy
// - List<Song> Songs

// In class User:
// - string UserId
// - string UserName
// - List<string> FavoriteGenres
// - List<Playlist> UserPlaylists

// In class MusicManager:
public void AddSong(string title, string artist, string genre, 
                   string album, TimeSpan duration)
public void CreatePlaylist(string userId, string playlistName)
public bool AddSongToPlaylist(string playlistId, string songId)
public Dictionary<string, List<Song>> GroupSongsByGenre()
public List<Song> GetTopPlayedSongs(int count)
Use Cases:
•	Add songs with metadata
•	Create and manage playlists
•	Group songs by genre
•	Track song popularity
•	User profile management
