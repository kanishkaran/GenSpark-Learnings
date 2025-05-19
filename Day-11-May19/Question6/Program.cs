
static void countFrequency(int[] arr)
{
    int Length = arr.Length;
    bool[] matched = new bool[Length];

    for (int i = 0; i < Length; i++)
    {
        if (matched[i])
            continue;

        int count = 1;

        for (int j = i + 1; j < Length; j++)
        {
            if (arr[i] == arr[j])
            {
                count++;
                matched[j] = true;
            }
        }

        Console.WriteLine($"{arr[i]} occured {count} times");
    }
}

Console.WriteLine("Please enter the size of the array: ");
int size;

while (!int.TryParse(Console.ReadLine(), out size))
    Console.WriteLine("Please enter valid size of array");

int[] array = new int[size];    
int num;

Console.WriteLine($"Please enter {size} numbers:");
for (int i = 0; i < size; i++)
{
    while (!int.TryParse(Console.ReadLine(), out num))
        Console.WriteLine("Please enter valid number");

    array[i] = num;
}


countFrequency(array);