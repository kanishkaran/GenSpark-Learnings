



static bool ValidateAllRows(int[,] arr)
{

    for (int row = 0; row < 9; row++)
    {

        int[] count = new int[10];
        for (int col = 0; col < 9; col++)
        {
            int num = arr[row, col];
            if (num < 1 || num > 9)
            {
                Console.WriteLine($"Does not contain valid numbers in row {row}");
                return false;
            }
            if (count[num] > 0)
            {
                Console.WriteLine("Contains duplicate in row");
                return false;
            }
            count[num]++;
        }
    }
    return true;
}

static bool ValidateAllColumns(int[,] arr)
{
    for (int col = 0; col < 9; col++)
    {
        int[] count = new int[10];
        for (int row = 0; row < 9; row++)
        {
            int num = arr[row, col];
            if (num < 1 || num > 9)
            {
                Console.WriteLine($"Does not contain valid numbers in column {col}");
                return false;
            }
            if (count[num] > 0)
            {
                Console.WriteLine($"Contains duplicate in columns {col}");
                return false;
            }
            count[num]++;
        }
    }
    return true;
}

static bool ValidateAllBlocks(int[,] arr)
{
    for (int blockRow = 0; blockRow < 3; blockRow++)
    {
        for (int blockCol = 0; blockCol < 3; blockCol++)
        {
            int[] count = new int[10];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int num = arr[blockRow * 3 + row, blockCol * 3 + col];
                    if (num < 1 || num > 9)
                    {
                        Console.WriteLine($"Does not contain valid numbers in block ({blockRow},{blockCol})");
                        return false;
                    }
                    if (count[num] > 0)
                    {
                        Console.WriteLine($"Contains duplicate in block ({blockRow},{blockCol})");
                        return false;
                    }
                    count[num]++;
                }
            }
        }
    }
    return true;
}


// int size = 9;

// int[,] board = new int[9, 9];


// for (int i = 0; i < 9; i++)
// {
//     int num;

//     Console.WriteLine($"row_{i}");
//     Console.WriteLine($"Please enter {size} numbers:");
//     for (int j = 0; j < size; j++)
//     {
//         while (!int.TryParse(Console.ReadLine(), out num))
//             Console.WriteLine("Kindly enter valid number");

//         board[i, j] = num;
//     }

// }

int[,] board_2 = new int[9, 9]
{
    {5,3,4,6,7,8,9,1,2},
    {6,7,2,1,9,5,3,4,8},
    {1,9,8,3,4,2,5,6,7},
    {8,5,9,7,6,1,4,2,3},
    {4,2,6,8,5,3,7,9,1},
    {7,1,3,9,2,4,8,5,6},
    {9,6,1,5,3,7,2,8,4},
    {2,8,7,4,1,9,6,3,5},
    {3,4,5,2,8,6,1,7,9}
};

if(ValidateAllRows(board_2) && ValidateAllColumns(board_2) && ValidateAllBlocks(board_2))
    Console.WriteLine("valid");
else
    Console.WriteLine("Invalid");
