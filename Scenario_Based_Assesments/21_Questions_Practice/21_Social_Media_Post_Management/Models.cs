using System;
using System.Collections.Generic;

namespace _21_Social_Media_Post_Management
{
    // Represents a user
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public int FollowersCount { get; set; }

        public List<string> Following { get; set; }
            = new List<string>();
    }

    // Represents a post
    public class Post
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
        public string PostType { get; set; } // Text / Image / Video
        public int Likes { get; set; }

        public List<string> Comments { get; set; }
            = new List<string>();

        // Prevent duplicate likes
        public HashSet<string> LikedUsers { get; set; }
            = new HashSet<string>();
    }
}
