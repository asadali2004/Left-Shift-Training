using System;
using System.Linq;

namespace _21_Social_Media_Post_Management
{
    class Program
    {
        static void Main(string[] args)
        {
            SocialMediaManager manager = new SocialMediaManager();

            // ✅ Hardcoded Users
            manager.RegisterUser("Asad", "Backend Developer");
            manager.RegisterUser("Rohit", "Tech Enthusiast");

            // ✅ Hardcoded Posts
            manager.CreatePost("U1", "Hello World!", "Text");
            manager.CreatePost("U2", "Check out this cool tech!", "Image");

            // Simulate interactions
            manager.LikePost("P1001", "U2");
            manager.LikePost("P1001", "U1");
            manager.AddComment("P1001", "U2", "Nice post!");

            while (true)
            {
                Console.WriteLine("\n=== Social Media Platform ===");
                Console.WriteLine("1. View Posts");
                Console.WriteLine("2. Create Post");
                Console.WriteLine("3. Like Post");
                Console.WriteLine("4. Comment On Post");
                Console.WriteLine("5. Group Posts By User");
                Console.WriteLine("6. View Trending Posts");
                Console.WriteLine("7. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    foreach (var p in manager.Posts.Values)
                    {
                        Console.WriteLine($"{p.PostId} | Likes: {p.Likes} | {p.Content}");
                    }
                }

                else if (choice == "2")
                {
                    Console.Write("User ID: ");
                    string uid = Console.ReadLine();

                    Console.Write("Content: ");
                    string content = Console.ReadLine();

                    Console.Write("Type (Text/Image/Video): ");
                    string type = Console.ReadLine();

                    manager.CreatePost(uid, content, type);

                    Console.WriteLine("Post created!");
                }

                else if (choice == "3")
                {
                    Console.Write("Post ID: ");
                    string pid = Console.ReadLine();

                    Console.Write("User ID: ");
                    string uid = Console.ReadLine();

                    manager.LikePost(pid, uid);

                    Console.WriteLine("Post liked!");
                }

                else if (choice == "4")
                {
                    Console.Write("Post ID: ");
                    string pid = Console.ReadLine();

                    Console.Write("User ID: ");
                    string uid = Console.ReadLine();

                    Console.Write("Comment: ");
                    string comment = Console.ReadLine();

                    manager.AddComment(pid, uid, comment);

                    Console.WriteLine("Comment added!");
                }

                else if (choice == "5")
                {
                    var grouped = manager.GroupPostsByUser();

                    foreach (var g in grouped)
                    {
                        Console.WriteLine($"\nUser: {g.Key}");

                        foreach (var post in g.Value)
                            Console.WriteLine($"  {post.Content} ({post.Likes} likes)");
                    }
                }

                else if (choice == "6")
                {
                    Console.Write("Minimum likes: ");
                    int likes = int.Parse(Console.ReadLine());

                    var trending = manager.GetTrendingPosts(likes);

                    if (!trending.Any())
                        Console.WriteLine("No trending posts!");
                    else
                    {
                        foreach (var p in trending)
                            Console.WriteLine($"{p.Content} | Likes: {p.Likes}");
                    }
                }

                else if (choice == "7")
                {
                    Console.WriteLine("Exiting Social Platform!");
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
