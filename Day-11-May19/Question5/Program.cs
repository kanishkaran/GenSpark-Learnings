



static void isDivisible(List<int> list)
{
    Console.WriteLine("The numbers divisible by 7 are: ");
    foreach (int num in list)
    {
        if (num % 7 == 0)
        {
            Console.Write($"{num} ");
        }
    }
}


Console.WriteLine("how many numbers to check?");
int count;
 while (!int.TryParse(Console.ReadLine(), out count))
        Console.WriteLine("Invalid, Please try again");
int num;
List<int> numbers = new List<int>();


for (int i = 0; i < count; i++)
{
    Console.WriteLine("Enter a number: ");

    while (!int.TryParse(Console.ReadLine(), out num))
        Console.WriteLine("Invalid, Please try again");

    numbers.Add(num);
}

isDivisible(numbers);