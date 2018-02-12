using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game State
    int level; //(member variable) integers start with default value of zero
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;
    
    // Use this for initialization
    void Start () {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Which one would you want to hack in?");
        Terminal.WriteLine("Press 1 to hack in CIA.");
        Terminal.WriteLine("Press 2 to hack in KGB");
        Terminal.WriteLine("Press 3 to hack in MOSSAD.");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input) {
        if (input == "menu") // we can always go direct to mainmenu
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
    }

    void RunMainMenu(string input) {
        if (input == "1")
        {
            level = 1;
            StartGame();
        }
        else if (input == "2")
        {
            level = 2;
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

    void StartGame() {
        currentScreen = Screen.Password;
        Terminal.WriteLine("You have chosen level " + level);
        Terminal.WriteLine("Please enter your password");
    }

 
}
