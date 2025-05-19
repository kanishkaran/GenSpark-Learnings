




static void rotate_array(int[] arr)
{
    int Length = arr.Length;
    int firstNum = arr[0];

    for (int i = 0; i < Length - 1; i++)
    {
        arr[i] = arr[i + 1];
    }

    arr[Length - 1] = firstNum;

    Console.WriteLine("The rotated array:");
    foreach (int num in arr)
    {
        Console.Write($"{num} ");
    }
}





Console.WriteLine("Please enter the size of the array: ");
int size;

while (!int.TryParse(Console.ReadLine(), out size))
    Console.WriteLine("Kindly enter valid size of array");

int[] array = new int[size];
int num;

Console.WriteLine($"Please enter {size} numbers:");
for (int i = 0; i < size; i++)
{
    while (!int.TryParse(Console.ReadLine(), out num))
        Console.WriteLine("Kindly enter valid number");

    array[i] = num;
}

rotate_array(array);