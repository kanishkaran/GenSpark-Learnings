
static void authenticate(string? username, string? password)
{
    if (username == "Admin" && password == "pass")
    {
        Console.WriteLine("Login Success");
    }
    else
    {

        string? user_name;
        string? pass;
        int attempts = 0;
        bool isAuthenticated = false;

        while (attempts < 3)
        {
            Console.WriteLine("The entered credentials are wrong! Please Try again");
            Console.WriteLine($"You have {3 - attempts} attempts left");

            Console.WriteLine("Enter username: ");
            user_name = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            pass = Console.ReadLine();

            if (user_name == "Admin" && pass == "pass")
            {
                Console.WriteLine("Login Success");
                isAuthenticated = true;
                break;
            }
            attempts++;
        }
        if (!isAuthenticated)
        {
            Console.WriteLine("Invalid attempts for 3 times exiting..");
        }
    }
}


Console.WriteLine("Enter username: ");
string? username = Console.ReadLine();

Console.WriteLine("Enter password: ");
string? password = Console.ReadLine();


authenticate(username, password);