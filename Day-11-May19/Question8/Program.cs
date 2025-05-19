
static void merge_arrays(int[] arr1, int[] arr2, int m, int n)
{
    int[] merged_arr = new int[m + n];

    for (int i = 0; i < m; i++)
    {
        merged_arr[i] = arr1[i];
    }

    for (int i = 0; i < n; i++)
    {
        merged_arr[m + i] = arr2[i];
    }

    Console.WriteLine("The merged array:");
    foreach (int num in merged_arr)
    {
        Console.Write($"{num} ");
    }

}

Console.WriteLine("Please enter the size of the array 1: ");
int size_arr_1;

while (!int.TryParse(Console.ReadLine(), out size_arr_1))
    Console.WriteLine("Kindly enter valid size of array");

int[] array_1 = new int[size_arr_1];
int num;

Console.WriteLine($"Please enter {size_arr_1} numbers:");
for (int i = 0; i < size_arr_1; i++)
{
    while (!int.TryParse(Console.ReadLine(), out num))
        Console.WriteLine("Kindly enter valid number");

    array_1[i] = num;
}


Console.WriteLine("Please enter the size of the array 2: ");
int size_arr_2;

while (!int.TryParse(Console.ReadLine(), out size_arr_2))
    Console.WriteLine("Kindly enter valid size of array");

int[] array_2 = new int[size_arr_2];
int number;

Console.WriteLine($"Please enter {size_arr_2} numbers:");
for (int i = 0; i < size_arr_2; i++)
{
    while (!int.TryParse(Console.ReadLine(), out number))
        Console.WriteLine("Kindly enter valid number");

    array_2[i] = number;
}


merge_arrays(array_1, array_2, size_arr_1, size_arr_2);