using System;
using Chess.ConsoleApp.Game;
using Chess.ConsoleApp.Game.SinglePlayerMode;
using Chess.ConsoleApp.Game.TwoPlayersMode;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;

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
            int choice = UserInteraction.GetNumberFromUser(
                "Select Mode: ", $"Option not found. Please try again.", 1, 2);
            if (choice == 1)
                return PlayerMode.SinglePlayer;
            return PlayerMode.TwoPlayers;
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