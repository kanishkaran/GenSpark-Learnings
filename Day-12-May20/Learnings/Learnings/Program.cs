// 1) Design a C# console app that uses a jagged array to store data for Instagram posts by multiple users. Each user can have a different number of posts, 
// and each post stores a caption and number of likes.

// You have N users, and each user can have M posts (varies per user).

// Each post has:

// A caption (string)

// A number of likes (int)

// Store this in a jagged array, where each index represents one user's list of posts.

// Display all posts grouped by user.



static void get_post(int n)
{
    string[][] captions = new string[n][];
    int[][] likes = new int[n][];
    int no_of_posts;

    for (int users = 0; users < n; users++)
    {
        Console.WriteLine($"User_{users + 1}  How many posts?");

        while (!int.TryParse(Console.ReadLine(), out no_of_posts))
            Console.WriteLine("enter a valid number!");

        captions[users] = new string[no_of_posts];
        likes[users] = new int[no_of_posts];
        int like;

        for (int post = 0; post < no_of_posts; post++)
        {
            Console.WriteLine($"Enter caption for post {post + 1}");

            captions[users][post] = Console.ReadLine() ?? " ";

            Console.WriteLine($"Enter likes for post {post + 1}");

            while (!int.TryParse(Console.ReadLine(), out like))
                Console.WriteLine("Enter valid likes");

            likes[users][post] = like;

        }
    }


    Console.WriteLine("--- Displaying Instagram Posts ---");

    for (int user = 0; user < n; user++)
    {
        Console.WriteLine($"User {user + 1}");

        for (int post = 0; post < captions[user].Length; post++)
        {
            Console.WriteLine($"Post {post + 1} - Caption: {captions[user][post]} | Likes: {likes[user][post]}");
        }
    }
}


int no_of_users;
Console.WriteLine("Please Enter number of users: ");
while (!int.TryParse(Console.ReadLine(), out no_of_users))
    Console.WriteLine("Kindly enter a valid number");


get_post(no_of_users);