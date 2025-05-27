# Twitter (Social Media) Database Model

## Code first Approach

The Application backend consist of the following models
- User
- UserFollow
- Tweet
- TweetLike
- TweetComment
- Hashtag
- TweetHashtag 


### User Model

Represents the Users of the application.

``` csharp
public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Tweet>? tweets;
        public ICollection<TweetLike>? tweetLikes;
        public ICollection<TweetComment>? tweetComments;
        public ICollection<TweetHashtag>? tweetHashtags;
        public ICollection<UserFollow>? followers;
        public ICollection<UserFollow>? following;
    }
```

Consist of *id*, *firstname*, *lastname*, *email*, *passwordhash*, *createdAt time* and Users can have number of tweets they posted, the likes the did, the comments they did, the hashtags they used and list of followers of the user and people the are following

### Tweet Model

``` csharp
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<TweetLike>? likes;

        public ICollection<TweetComment>? comments;

        public ICollection<TweetHashtag>? hashtags;
    }
```
Consist of foreign key relation to the user by referencing userId. This gives the tweets posted by a user.
This tweets can have no of likes, comments and hashtags.

### TweetLike Model

``` csharp
public class TweetLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }

    }
```
Establishes relationship b/w User model and tweet model. A many-to-many relationship where a user can like many tweet and a tweet can be liked by many users.

### Hashtag Model

``` csharp
    public class Hashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
    }
```

This acts as a master table to tweethashtag model. Contains Unique hashtags which can be referenced by id in tweets.

### TweetHashtag Model

``` csharp
public class TweetHashtag
    {
        [Key]
        public int TweetId { get; set; }
        public int HashtagId { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }

        [ForeignKey("HashtagId")]
        public Hashtag? Hashtag { get; set; }
    }
```

Establishes relationship b/w tweet and hashtag. This lets us know what hashtags are used in what tweets.
Since hashtagId is used, the repeatition of tag i.e string is not necessary.

### UserFollow Model

``` csharp
public class UserFollow
    {
        [Key]
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }

        [ForeignKey("FollowerId")]
        public User? Follower { get; set; }
        [ForeignKey("FollowingId")]
        public User? Following { get; set; }
    }
```

Makes a relationship b/w users. A user can be followed by several other users and a user can also follow many users.
A many-to-many relationship between users.
