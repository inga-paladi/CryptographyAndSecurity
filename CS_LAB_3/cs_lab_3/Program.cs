using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public class VigenereCipher
{
    private static readonly string alphabet = "AĂÂBCDEFGHIÎJKLMNOPQRSȘTȚUVWXYZ";

    public static void Main()
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1 - Encryption");
            Console.WriteLine("2 - Decryption");
            Console.WriteLine("3 - Exit");

            int choice = GetValidChoice(1, 3);

            switch (choice)
            {
                case 1:
                    string message = GetValidInput("Enter the message to encrypt: ");
                    string vigenereKey = GetValidKey("Enter the Vigenere key (at least 7 characters): ");
                    string encryptedMessage = Encrypt(message, vigenereKey);
                    Console.WriteLine("Encrypted message: " + encryptedMessage);
                    break;

                case 2:
                    string cryptogram = GetValidInput("Enter the message to decrypt: ");
                    string vigenereKey2 = GetValidKey("Enter the Vigenere key (at least 7 characters): ");
                    string decryptedMessage = Decrypt(cryptogram, vigenereKey2);
                    Console.WriteLine("Decrypted message: " + decryptedMessage);
                    break;

                case 3:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static private string GetValidInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";
            input = input.ToUpper();
            if (Regex.IsMatch(input, @"^[A-ZĂÂÎȘȚ ]+$"))
                return string.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        }
    }

    static string GetValidKey(string prompt)
    {
        while (true)
        {
            var input = GetValidInput(prompt);
            if (input.Length >= 7)
                return input;
        }
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

    static public string Encrypt(string message, string key)
    {
        return VigenereCipherOperation(message, key, true);
    }

    static public string Decrypt(string cryptogram, string key)
    {
        return VigenereCipherOperation(cryptogram, key, false);
    }

    static private string VigenereCipherOperation(string message, string key, bool encrypt)
    {
        string result = "";

        for (int messageIndex = 0; messageIndex < message.Length; ++messageIndex)
        {
            char messageCharToUse = message[messageIndex];
            int messageCharIndexToUse = alphabet.IndexOf(messageCharToUse);
            char keyCharToUse = key[messageIndex % key.Length];
            int keyCharIndexToUse = alphabet.IndexOf(keyCharToUse);

            int resultIndex;
            if (encrypt)
                resultIndex = (keyCharIndexToUse + messageCharIndexToUse) % alphabet.Length;
            else
                resultIndex = Math.Abs(messageCharIndexToUse + alphabet.Length - keyCharIndexToUse) % alphabet.Length;
            char resultChar = alphabet[resultIndex];
            result += resultChar;
        }

        return result;
    }
}
