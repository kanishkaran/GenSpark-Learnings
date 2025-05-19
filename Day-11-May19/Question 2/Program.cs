
// 2) Take 2 numbers from user and print the largest
static int get_largest(int num_1, int num_2)
{
    return num_1 > num_2 ? num_1 : num_2;
}



Console.WriteLine("Enter the first number: ");
int number_1 = int.Parse(Console.ReadLine());

Console.WriteLine("Enter the second number: ");
int number_2 = int.Parse(Console.ReadLine());

int largest = get_largest(number_1, number_2);

Console.WriteLine($"The largest is {largest}");