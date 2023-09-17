using System;
using System.Text.RegularExpressions;

class CaesarCipher
{
    static void Main()
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1 - Encryption with one key");
            Console.WriteLine("2 - Decryption with one key");
            Console.WriteLine("3 - Encryption with two keys");
            Console.WriteLine("4 - Decryption with two keys");
            Console.WriteLine("5 - Exit");

            int choice = GetValidChoice(1, 5);

            switch (choice)
            {
                case 1:
                    string inputMessage1 = GetValidInput("Enter the message to encrypt: ", "^[A-Z]*$");
                    int caesarKey1 = GetValidKey("Enter the Caesar key (1-25): ", 1, 25);
                    string encryptedMessage1 = CaesarCipherOperation(inputMessage1, caesarKey1, "", true);
                    Console.WriteLine("Encrypted message: " + encryptedMessage1);
                    break;

                case 2:
                    string inputMessage2 = GetValidInput("Enter the message to decrypt: ", "^[A-Z]*$");
                    int caesarKey2 = GetValidKey("Enter the Caesar key (1-25): ", 1, 25);
                    string decryptedMessage2 = CaesarCipherOperation(inputMessage2, caesarKey2, "", false);
                    Console.WriteLine("Decrypted message: " + decryptedMessage2);
                    break;

                case 3:
                    string inputMessage3 = GetValidInput("Enter the message to encrypt: ", "^[A-Z]*$");
                    int caesarKey3 = GetValidKey("Enter the Caesar key (1-25): ", 1, 25);
                    string permutationKey3 = GetValidInput("Enter the permutation key (at least 7 characters): ", "^[A-Z]{7,}$");
                    string encryptedMessage3 = CaesarCipherOperation(inputMessage3, caesarKey3, permutationKey3, true);
                    Console.WriteLine("Encrypted message: " + encryptedMessage3);
                    Console.WriteLine("Permuted Alphabet: " + GeneratePermutedAlphabet(alphabet, permutationKey3));
                    break;

                case 4:
                    string inputMessage4 = GetValidInput("Enter the message to decrypt: ", "^[A-Z]*$");
                    int caesarKey4 = GetValidKey("Enter the Caesar key (1-25): ", 1, 25);
                    string permutationKey4 = GetValidInput("Enter the permutation key (at least 7 characters): ", "^[A-Z]{7,}$");
                    string decryptedMessage4 = CaesarCipherOperation(inputMessage4, caesarKey4, permutationKey4, false);
                    Console.WriteLine("Decrypted message: " + decryptedMessage4);
                    Console.WriteLine("Permuted Alphabet: " + GeneratePermutedAlphabet(alphabet, permutationKey4));
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static string GetValidInput(string prompt, string pattern)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine().ToUpper().Replace(" ", "");
        } while (!Regex.IsMatch(input, pattern));
        return input;
    }

    static int GetValidChoice(int minValue, int maxValue)
    {
        int choice;
        do
        {
            Console.Write("Enter your choice: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue);
        return choice;
    }

    static int GetValidKey(string prompt, int minValue, int maxValue)
    {
        int key;
        do
        {
            Console.Write(prompt);
        } while (!int.TryParse(Console.ReadLine(), out key) || key < minValue || key > maxValue);
        return key;
    }

    static string CaesarCipherOperation(string message, int caesarKey, string permutationKey, bool encrypt)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string permutedAlphabet = GeneratePermutedAlphabet(alphabet, permutationKey);
        string result = "";

        foreach (char letter in message)
        {
            if (char.IsLetter(letter))
            {
                int index = permutedAlphabet.IndexOf(letter);
                int offset = encrypt ? caesarKey : -caesarKey;
                int newIndex = (index + offset + 26) % 26;
                char transformedLetter = permutedAlphabet[newIndex];
                result += transformedLetter;
            }
            else
            {
                result += letter;
            }
        }

        return result;
    }

    static string GeneratePermutedAlphabet(string alphabet, string permutationKey)
    {
        permutationKey = permutationKey.ToUpper();
        string permutedAlphabet = "";

        foreach (char letter in permutationKey)
        {
            if (char.IsLetter(letter) && !permutedAlphabet.Contains(letter.ToString()))
            {
                permutedAlphabet += letter;
            }
        }

        foreach (char letter in alphabet)
        {
            if (!permutedAlphabet.Contains(letter.ToString()))
            {
                permutedAlphabet += letter;
            }
        }

        return permutedAlphabet;
    }
}
