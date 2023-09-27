using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hacker : MonoBehaviour
{

    //Game State
    int level;
    enum Screen { MainMenu, Password, Decryption, Win };
    Screen currentScreen = Screen.MainMenu;

    const int maxAttempts = 3;
    int attemptsRemaining = maxAttempts;

    string password;
    string hint;
    string encryptedMessage = "QZJGS"; // Example message
    string decryptionKey;

    Dictionary<int, string[]> passwordDatabase = new Dictionary<int, string[]> {
        { 1, new[] { "spy", "agent", "mission" } },
        { 2, new[] { "sputnik", "russia", "coldwar" } },
        { 3, new[] { "israel", "intelligence", "espionage" } }
    };

    void Start()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        attemptsRemaining = maxAttempts;
        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to Terminal Hacker!");
        Terminal.WriteLine("Which secret database would you want to hack into?");
        Terminal.WriteLine("Press 1 to hack into CIA.");
        Terminal.WriteLine("Press 2 to hack into KGB.");
        Terminal.WriteLine("Press 3 to hack into MOSSAD.");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.Decryption)
        {
            CheckDecryptionKey(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevel = int.TryParse(input, out level) && passwordDatabase.ContainsKey(level);
        if (isValidLevel)
        {
            StartPasswordGuess();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Welcome back, Mr. Bond.");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    void StartPasswordGuess()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine($"Enter the password for level {level}.");
        Terminal.WriteLine($"Hint: {hint}");
        Terminal.WriteLine($"Attempts left: {attemptsRemaining}");
    }

    void SetRandomPassword()
    {
        string[] passwordsForLevel = passwordDatabase[level];
        int passwordIndex = UnityEngine.Random.Range(0, passwordsForLevel.Length);
        password = passwordsForLevel[passwordIndex];
        hint = string.Join("", password.ToCharArray().OrderBy(g => UnityEngine.Random.Range(0, password.Length)).ToArray());
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            StartDecryptionTask();
        }
        else
        {
            attemptsRemaining--;
            if (attemptsRemaining <= 0)
            {
                Terminal.WriteLine("All attempts exhausted. Please try again.");
                ShowMainMenu();
            }
            else
            {
                Terminal.WriteLine($"Incorrect! Attempts left: {attemptsRemaining}");
                Terminal.WriteLine($"Hint: {hint}");
            }
        }
    }

    void StartDecryptionTask()
    {
        currentScreen = Screen.Decryption;
        Terminal.ClearScreen();
        encryptedMessage = EncryptMessage(password, (level + password.Length)); // Encrypting password based on level and password length
        decryptionKey = (level + password.Length).ToString();
        Terminal.WriteLine($"Access granted to level {level} database!");
        Terminal.WriteLine("However, the data is encrypted. Please decrypt to access.");
        Terminal.WriteLine($"Encrypted message: {encryptedMessage}");
        Terminal.WriteLine("Enter the decryption key:");
    }

    void CheckDecryptionKey(string input)
    {
        if (DecryptMessage() == password)
        {
            Terminal.WriteLine("Decryption successful! Accessing data...");
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect decryption key. Try again or type 'menu' to exit.");
        }
    }


    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine($"Successfully hacked level {level} database!");
        Terminal.WriteLine($"Message: {DecryptMessage()}");
        Terminal.WriteLine("Press 'menu' to hack another.");
    }

    string EncryptMessage(string message, int shiftAmount)
    {
        char[] encryptedChars = message.Select(c =>
        {
            if (char.IsUpper(c))
            {
                return (char)(((c - 'A' + shiftAmount) % 26) + 'A');
            }
            else if (char.IsLower(c))
            {
                return (char)(((c - 'a' + shiftAmount) % 26) + 'a');
            }
            return c;
        }).ToArray();
        return new string(encryptedChars);
    }

    string DecryptMessage()
    {
        // Simple Caesar cipher for illustration.
        char[] decryptedChars = encryptedMessage.Select(c =>
        {
            if (char.IsUpper(c))
            {
                return (char)(((c - 'A' + 26 - int.Parse(decryptionKey)) % 26) + 'A');
            }
            else if (char.IsLower(c))
            {
                return (char)(((c - 'a' + 26 - int.Parse(decryptionKey)) % 26) + 'a');
            }
            return c;
        }).ToArray();
        return new string(decryptedChars);
    }
}
