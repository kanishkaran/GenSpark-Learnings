

static bool IsLower(string input)
{
    foreach (char c in input)
    {
        if (!char.IsLower(c))
            return false;
    }
    return true;
}


static string Encrypt(string input,int shift)
{
    string encrypted_text = "";

    foreach (char c in input)
    {
        encrypted_text += (char)(((c - 'a' + shift) % 26) + 'a');
    }

    return encrypted_text;
}

static string Decrypt(string input,int shift)
{
    string decrypted_text = "";

    foreach (char c in input)
    {
        decrypted_text += (char)(((c - 'a' - shift + 26 ) % 26) + 'a');
    }

    return decrypted_text;
}

Console.WriteLine("Please Enter Word to Encrypt: ");

string? input = Console.ReadLine();

while (string.IsNullOrWhiteSpace(input) || !IsLower(input))
{
    Console.WriteLine("Enter Valid input");
    input = Console.ReadLine();
}

int shift;
 Console.WriteLine("Please Enter shift value");
while (!int.TryParse(Console.ReadLine(), out shift))
    Console.WriteLine("Please Enter valid shift value");


string enc_text = Encrypt(input, shift);

Console.WriteLine($"The Encrypted text: {enc_text}");

string dec_text = Decrypt(enc_text, shift);

Console.WriteLine($"The decrypted text: {dec_text}");
