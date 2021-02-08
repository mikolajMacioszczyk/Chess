using System;
using Chess.ConsoleApp.Enums;
using Chess.ConsoleApp.Game;
using Chess.ConsoleApp.Game.SinglePlayerMode;
using Chess.ConsoleApp.Game.TwoPlayersMode;
using Chess.ConsoleApp.Helpers;

namespace Chess.ConsoleApp.Application
{
    public static class NewGameManager
    {
        private static void ShowModeMenu()
        {
            Console.WriteLine(" ======================== Select Mode ========================");
            Console.WriteLine("1. Single Player");
            Console.WriteLine("2. Two Players");
        }

        private static PlayerMode SelectMode()
        {
            ShowModeMenu();
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "Select Mode: ", "Expected positive number, please try again.");
            switch (choice)
            {
                case 1: return PlayerMode.SinglePlayer;
                case 2: return PlayerMode.TwoPlayers;
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again.");
                    return SelectMode();
            }
        }
        
        static IConsoleGame GetGameManager()
        {
            switch (SelectMode())
            {
                case PlayerMode.SinglePlayer: return new SinglePlayerModeConsoleGame();
                case PlayerMode.TwoPlayers: return new TwoPlayersModeConsoleGame();
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        public static void Run()
        {
            IConsoleGame gameManager = GetGameManager();
            gameManager.Start();
        }
    }
}