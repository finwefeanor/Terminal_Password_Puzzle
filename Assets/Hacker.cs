using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hacker : MonoBehaviour
{

    //Game State
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;

    const int maxAttempts = 3;
    int attemptsRemaining = maxAttempts;

    string password;
    string hint;

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
        Terminal.WriteLine("Which one would you want to hack in?");
        Terminal.WriteLine("Press 1 to hack in CIA.");
        Terminal.WriteLine("Press 2 to hack in KGB");
        Terminal.WriteLine("Press 3 to hack in MOSSAD.");
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
    }

    void RunMainMenu(string input)
    {
        bool isValidLevel = int.TryParse(input, out level) && passwordDatabase.ContainsKey(level);
        if (isValidLevel)
        {
            StartGame();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("What can i do for you Mr Bond?");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine($"Enter the password for level {level}");
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
            Terminal.WriteLine("Congratulations! Access granted.");
            currentScreen = Screen.Win;
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
}
