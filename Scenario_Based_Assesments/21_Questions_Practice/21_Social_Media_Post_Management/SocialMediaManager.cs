using System;
using System.Collections.Generic;
using System.Linq;

namespace _21_Social_Media_Post_Management
{
    public class SocialMediaManager
    {
        public Dictionary<string, User> Users
            = new Dictionary<string, User>();

        public Dictionary<string, Post> Posts
            = new Dictionary<string, Post>();

        private int nextUserId = 1;
        private int nextPostId = 1001;

        // Register user
        public void RegisterUser(string userName, string bio)
        {
            string id = "U" + nextUserId++;

            Users.Add(id, new User
            {
                UserId = id,
                UserName = userName,
                Bio = bio,
                FollowersCount = 0
            });
        }

        // Create post
        public void CreatePost(string userId, string content, string type)
        {
            if (!Users.ContainsKey(userId))
                return;

            string postId = "P" + nextPostId++;

            Posts.Add(postId, new Post
            {
                PostId = postId,
                UserId = userId,
                Content = content,
                PostTime = DateTime.Now,
                PostType = type,
                Likes = 0
            });
        }

        // Like post
        public void LikePost(string postId, string userId)
        {
            if (!Posts.ContainsKey(postId) || !Users.ContainsKey(userId))
                return;

            var post = Posts[postId];

            // Avoid duplicate likes
            if (post.LikedUsers.Add(userId))
            {
                post.Likes++;
            }
        }

        // Add comment
        public void AddComment(string postId, string userId, string comment)
        {
            if (!Posts.ContainsKey(postId) || !Users.ContainsKey(userId))
                return;

            Posts[postId].Comments
                .Add($"{Users[userId].UserName}: {comment}");
        }

        // Group posts by user
        public Dictionary<string, List<Post>> GroupPostsByUser()
        {
            return Posts.Values
                .GroupBy(p => Users[p.UserId].UserName)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Trending posts
        public List<Post> GetTrendingPosts(int minLikes)
        {
            return Posts.Values
                .Where(p => p.Likes >= minLikes)
                .OrderByDescending(p => p.Likes)
                .ToList();
        }
    }
}
