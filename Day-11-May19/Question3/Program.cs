
static void calculate(int num_1, int num_2)
{
    Console.WriteLine("Enter one operation to perform: ");
    Console.WriteLine("1.Add \n2.Subtract \n3.Divide \n4.Multiple");
    int operation = int.Parse(Console.ReadLine());

    switch (operation)
    {
        case 1:
            Console.WriteLine(num_1 + num_2);
            break;
        case 2:
            Console.WriteLine(num_1 - num_2);
            break;
        case 3:
            Console.WriteLine(num_1 / num_2);
            break;
        case 4:
            Console.WriteLine(num_1 * num_2);
            break;
        default:
            Console.WriteLine("Enter valid option");
            break;
    }
}

Console.WriteLine("Enter the first number: ");
int number_1 = int.Parse(Console.ReadLine());

Console.WriteLine("Enter the second number: ");
int number_2 = int.Parse(Console.ReadLine());

calculate(number_1, number_2);
