


static void check_match(string ans, string guess, int attempts)
{
    if (ans == guess)
    {
        Console.WriteLine($"{guess} 4 Bulls, 0 Cows Exact Match");
        Console.WriteLine($"{attempts} attempts");
        return;
    }
    int bulls = 0, cows = 0;
    bool[] ansMatched = new bool[4];
    bool[] guessMatched = new bool[4];
    string matched = "", misplaced = "";

    for (int i = 0; i < 4; i++)
    {
        if (ans[i] == guess[i])
        {
            bulls++;
            matched += ans[i] + ", ";
            ansMatched[i] = guessMatched[i] = true;
        }
    }

    for (int i = 0; i < 4; i++)
    {
        if (guessMatched[i])
            continue;

        for (int j = 0; j < 4; j++)
        {
            if (!ansMatched[j] && guess[i] == ans[j])
            {
                cows++;
                misplaced += guess[i] + ", ";
                ansMatched[j] = true;
                break;
            }
        }
    }

    if (bulls == 1)
        Console.WriteLine($"{guess}, {bulls} Bull, {cows} Cows");
    else
        Console.WriteLine($"{guess}, {bulls} Bulls, {cows} Cows");


    if (cows == 0)
        Console.WriteLine("right position " + (string.IsNullOrEmpty(matched) ? "-" : matched) + " rest wrong");
    else
        Console.WriteLine("right position " + (string.IsNullOrEmpty(matched) ? "-" : matched) + " misplaced " + (string.IsNullOrEmpty(misplaced) ? "-" : misplaced));


    Console.WriteLine("Try Another Guess");
    string? new_guess;
    new_guess = Console.ReadLine();

    while (string.IsNullOrWhiteSpace(new_guess) || new_guess.Length != 4)
    {
        Console.WriteLine("Invalid Input, Please provide valid guess: ");
        new_guess = Console.ReadLine();
    }

    check_match(ans, new_guess.ToUpper(), attempts + 1);

}


string answer = "GAME";

Console.WriteLine("Welcome to the Game");

Console.WriteLine("Please enter your guess of 4 characters: ");

string? guess = Console.ReadLine();

while (string.IsNullOrWhiteSpace(guess) || guess.Length != 4)
{
    Console.WriteLine("Invalid Input, Please provide valid guess: ");
    guess = Console.ReadLine();
}

check_match(answer, guess.ToUpper(), 1);