using System;
using Chess.ConsoleApp.Application;
using Chess.ConsoleApp.Helpers;

namespace Chess.ConsoleApp
{
    class Program
    {
        static void ShowIntroductionMenu()
        {
            Console.WriteLine(" ======================== Welcome ========================");
            Console.WriteLine(" 1. New Game");
            Console.WriteLine(" 2. Read Game");
            Console.WriteLine(" 3. Quit");
        }
        
        static void Main(string[] args)
        {
            int choice = 0;
            const int choiceEnd = 3; 
            while (choice != choiceEnd)
            {
                ShowIntroductionMenu();
                choice = UserInteraction.GetPositiveNumberFromUser(
                    "Hello! Select action: ", "Expected positive number, please try again.");
                switch (choice)
                {
                    case 1: 
                        NewGameManager.Run();
                        break;
                    case 2:
                        ReadGameManager.Run();
                        break;
                    case choiceEnd:
                        break;
                    default:
                        Console.WriteLine($"Option {choice} not found. Please try again.");
                        break;
                }
            }
        }
    }
}