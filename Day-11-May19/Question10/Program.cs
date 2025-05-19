



static bool ValidateRow(int[] arr)
{
    int[] count = new int[10];

    foreach (int num in arr)
    {
        if (num < 1 || num > 9)
        {
            Console.WriteLine("Does not contain valid numbers");
            return false;
        }
        if (count[num] > 0)
        {
            Console.WriteLine("Contains duplicate");
            return false;
        }
        count[num]++;
    }
    return true;
}

int size = 9;

int[] array = new int[size];
int num;

Console.WriteLine($"Please enter {size} numbers:");
for (int i = 0; i < size; i++)
{
    while (!int.TryParse(Console.ReadLine(), out num))
        Console.WriteLine("Kindly enter valid number");

    array[i] = num;
}

if (ValidateRow(array))
    Console.WriteLine("Valid");
else
    Console.WriteLine("Invalid");
