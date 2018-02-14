using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    //Game configuration data
    string[] level1Passwords = {"ship", "captain", "eyepatch", "treasure", "kraken", "rum"};
    string[] level2Passwords = { "informant", "gulag", "recon", "classified", "soldier", "covertops" };
    string[] level3Passwords = { "wasteland", "radaway", "pipboy", "fatman", "overseer", "radiation" };

    //Game State
    int level; //(member variable) integers start with default value of zero
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    
    // Use this for initialization
    void Start () {
        ShowMainMenu();
    }

    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Which one would you want to hack in?");
        Terminal.WriteLine("Press 1 to hack in Black Pearl");
        Terminal.WriteLine("Press 2 to hack in CIA");
        Terminal.WriteLine("Press 3 to hack in Vault-Tec");
        Terminal.WriteLine("");
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
            AskingPassword();
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

    void AskingPassword() {
        Terminal.ClearScreen();
        currentScreen = Screen.Password;
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine("");
        Terminal.WriteLine("*(type 'menu' to go back) ");
    }

    void SetRandomPassword() {
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
                index = UnityEngine.Random.Range(0, level3Passwords.Length);
                password = level3Passwords[index];
                break;
            default:
                Debug.LogError("Intruder!");
                break;
        }
    }

    void CheckPassword(string input) 
    {
        if (input == password)
        {
            Win();
        }
        else
        {
            AskingPassword();
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
                Terminal.WriteLine("Adventure awaits! ");
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

hit 'enter' to go back
"              );
                break;
            case 2:
                Terminal.WriteLine("Trespassers! You are under arrest! ");
                Terminal.WriteLine(@"
WAR,
 WAR NEVER CHANGES
__      _____
-=|_______|___
        (____|)             \☻/\☻/
      ░░ ▌░ ▌                ▌░ ▌
░░░                         / \/ \
          ███████]▄▄▄▄▄▄▄▄▄----------●
      ▂▄▅█████████▅▄▃▂
   I███████████████████].

hit 'enter' to go back
");
                break;
            case 3:
                Terminal.WriteLine("Radiation detected. Use Radaway! ");
                Terminal.WriteLine(@"
     (  )  _____  (  )         
      \ \/       \/ /
       \| []   [] |/
        (_  /^\  _)
          \  -  /
         / xxxxx \
       _/ /{___}\ \_
      (_ _)     (_ _)

hit 'enter' to go back
");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
        
    }

}
