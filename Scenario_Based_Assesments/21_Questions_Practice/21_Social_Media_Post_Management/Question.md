
# Question 21: Social Media Post Management

## Scenario
A social media platform needs to manage users, posts, and interactions.

---

## Requirements

### Class: `User`
- `string UserId`
- `string UserName`
- `string Bio`
- `int FollowersCount`
- `List<string> Following`

---

### Class: `Post`
- `string PostId`
- `string UserId`
- `string Content`
- `DateTime PostTime`
- `string PostType` (Text / Image / Video)
- `int Likes`
- `List<string> Comments`

---

### Class: `SocialMediaManager`

```csharp
public void RegisterUser(string userName, string bio);
public void CreatePost(string userId, string content, string type);
public void LikePost(string postId, string userId);
public void AddComment(string postId, string userId, string comment);
public Dictionary<string, List<Post>> GroupPostsByUser();
public List<Post> GetTrendingPosts(int minLikes);
````

---

## Use Cases

* User registration
* Create different types of posts
* Like and comment on posts
* Group posts by user
* Find trending posts

