
// 1) create a program that will take name from user and greet the user
static void greet_user(string? name)
{
    Console.WriteLine($"Hello {name}, Welcome to C#");
}


Console.WriteLine("Enter your name: ");
string? name = Console.ReadLine();
greet_user(name);


