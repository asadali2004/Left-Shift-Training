# StreamBuzz Engagement Tracker

You are developing a C# console application for **StreamBuzz**, a digital content platform that tracks creators' engagement over a period of 4 weeks. Each creator's name and weekly like counts are recorded using the `CreatorStats` class.

## Functionalities

### CreatorStats Properties

| Datatype | Property |
|---|---|
| `string` | `CreatorName` |
| `double[]` | `WeeklyLikes` |

> `public static List<CreatorStats> EngagementBoard` is provided in the template.

### Program Methods

| Method | Description |
|---|---|
| `public void RegisterCreator(CreatorStats record)` | Registers a creator record into `EngagementBoard`. |
| `public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)` | Counts the number of weeks each creator's `WeeklyLikes` are greater than or equal to `likeThreshold`. Returns a dictionary of creator name → count. If no creator meets the threshold, returns an empty dictionary. |
| `public double CalculateAverageLikes()` | Calculates and returns the average of all weekly likes across all creators. |

## Main Method Requirements

1. Get values from the user.
2. Call methods and display results.
3. **Choice 1:** Prompt for creator name and 4 weekly likes, then print **"Creator registered successfully"**.
4. **Choice 2:** Prompt for threshold, call `GetTopPostCounts`, and print results. If empty, display **"No top-performing posts this week"**.
5. **Choice 3:** Print **"Overall average weekly likes: <average>"**.
6. **Choice 4:** Print **"Logging off - Keep Creating with StreamBuzz!"** and terminate.

## Notes

- Keep class and methods `public`.
- Do not use `Environment.Exit()`.
- Do not change the given code template.

## Sample Input/Output

```
1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
1
Enter Creator Name:
Nicky
Enter weekly likes (Week 1 to 4):
1500
1600
1800
2000
Creator registered successfully

1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
1
Enter Creator Name:
Roma
Enter weekly likes (Week 1 to 4):
800
1000
1300
1400
Creator registered successfully

1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
2
Enter like threshold:
1400
Nicky - 4
Roma - 1

1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
2
Enter like threshold:
5000
No top-performing posts this week

1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
3
Overall average weekly likes: 1425

1. Register Creator
2. Show Top Posts
3. Calculate Average Likes
4. Exit
Enter your choice:
4
Logging off - Keep Creating with StreamBuzz!
```

