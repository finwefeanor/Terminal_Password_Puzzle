using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    //Game configuration data
    string[] level1Passwords = {"informant", "gulag", "recon", "classified", "payback"};
    string[] level2Passwords = { "prisoner", "arrest", "glock", "response", "copy", "farm" };

    //Game State
    int level; //(member variable) integers start with default value of zero
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    
    // Use this for initialization
    void Start () {
        ShowMainMenu();
    }

    void Update() {

    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Which one would you want to hack in?");
        Terminal.WriteLine("Press 1 to hack in CIA.");
        Terminal.WriteLine("Press 2 to hack in KGB");
        Terminal.WriteLine("Press 3 to hack in MI6");
        //Terminal.WriteLine("Press 3 to hack in MOSSAD.");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input) {
        if (input.Equals("menu", System.StringComparison.OrdinalIgnoreCase)) //ignores Capital word
            //(input == "menu") // we can always go direct to mainmenu
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
        else if (currentScreen == Screen.Win)
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu();
        }
    }

    void RunMainMenu(string input) {
        bool isValidLevel = (input == "1" || input == "2" || input == "3");
        if (isValidLevel)
        {
            level = int.Parse(input); //this turns "string input" to "integer" and puts it in variable(level)
            StartGame();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("What can i do for you Mr Bond?");
        }
        else
        {
            ShowMainMenu();
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    void StartGame() {
        Terminal.ClearScreen();
        currentScreen = Screen.Password;
        int index;
        switch (level)
        {
            case 1:
                index = UnityEngine.Random.Range(0, level1Passwords.Length);
                password = level1Passwords[index];
                break;
            case 2:
                index = UnityEngine.Random.Range(0, level2Passwords.Length);
                password = level2Passwords[index];
                break;
            case 3:
                index = UnityEngine.Random.Range(0, level2Passwords.Length);
                password = level2Passwords[index];
                break;
            default:
                Debug.LogError("Intruder!");
                break;
        }
        Terminal.WriteLine("Please enter your password");
    }

    void CheckPassword(string input) 
    {
        if (input == password)
        {
            Win();
        }
        else
        {
            Terminal.WriteLine("Wrong Password! Try again ");
        }
    }

    void Win() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowReward();
    }

    void ShowReward() {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Bravo! Access Granted ");
                Terminal.WriteLine(@"
            (((%%)))
         ((oo#%%%(=oooo0ooo0
       ((oo%%%%%%)       000
    ((((oo%%oo%%#)        000 
    (( _________ ))        ooo
   ((( |(@)__(@)| )))       00
  (_) | // █ //|  (?)
      (|\ | |/|)    ??
        (_===_)      ?

"               );
                break;
            case 2:
                Terminal.WriteLine("You are intelligent! ");
                Terminal.WriteLine(@"
WAR,
 WAR NEVER CHANGES
           \☻/\☻/
░░░░░░░░░░░░▌░ ▌
░░░░░░░░░░░/ \/ \
          ███████]▄▄▄▄▄▄▄▄▄----------●
      ▂▄▅█████████▅▄▃▂
   I███████████████████].
"               );
                break;
        }
        
    }

}
