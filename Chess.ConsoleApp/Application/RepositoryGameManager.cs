using System;
using System.Collections.Generic;
using System.Linq;
using Chess.ConsoleApp.Game.SinglePlayerMode;
using Chess.ConsoleApp.Game.TwoPlayersMode;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.GameSaver;

namespace Chess.ConsoleApp.Application
{
    public static class RepositoryGameManager
    {
        private struct ReadChessGameStateViewModel
        {
            public ChessGameState GameState { get; set; }
            public bool Succeeded { get; set; }
        }
        
        public static void Read()
        {
            var lastGameStateVm = TryReadGame();
            if (lastGameStateVm.Succeeded)
            {
                Console.WriteLine("Game read.");
                var state = lastGameStateVm.GameState;
                Console.WriteLine($"Is ended: {state.IsEnded}");
                string mode = state.PlayerMode == PlayerMode.SinglePlayer ? "Single Player" : "Two Players";
                Console.WriteLine($"Mode: {mode}");
                Console.WriteLine($"Current moving team: {state.CurrentMovingTeam}");

                if (state.PlayerMode == PlayerMode.TwoPlayers)
                {
                    new TwoPlayersModeConsoleGame(state).Start();
                }
                else
                {
                    new SinglePlayerModeConsoleGame(state).Start();
                }
            }
        }

        readonly struct RepoFilesChoice
        {
            public SaveRepository Repository { get; }
            public List<string> Files { get;  }
            public int Choice { get;  }

            public RepoFilesChoice(SaveRepository repository, List<string> files, int choice)
            {
                Repository = repository;
                Files = files;
                Choice = choice;
            }
        }

        public static void Delete()
        {
            var repoFilesChoice = CreateRepositoryFilesAndGetUserChoice("Select game to be deleted: ");

            int choice = repoFilesChoice.Choice;
            if (choice != 0)
            {
                repoFilesChoice.Repository.Delete(repoFilesChoice.Files[choice-1]);
            }
        }

        private static RepoFilesChoice CreateRepositoryFilesAndGetUserChoice(string message)
        {
            var repo = SaveRepository.GetDefaultRepository();
            var files = ShowSavedGamesAndReturnAsList(repo);
            var choice = UserInteraction.GetNumberFromUser(
                message, "Option not found. Please try again", 0, files.Count);

            return new RepoFilesChoice(repo, files, choice);
        }
        
        private static ReadChessGameStateViewModel TryReadGame()
        {
            var repoFilesChoice = CreateRepositoryFilesAndGetUserChoice("Select which game would you read:");
            int choice = repoFilesChoice.Choice;
            if (choice == 0)
            {
                return new ReadChessGameStateViewModel() {Succeeded = false};
            }
            return new ReadChessGameStateViewModel()
            {
                Succeeded = true,
                GameState = repoFilesChoice.Repository.Read(repoFilesChoice.Files[choice-1])
            };
        }

        private static List<string> ShowSavedGamesAndReturnAsList(SaveRepository repo)
        {
            Console.WriteLine("Saved games: ");
            var files = repo.GetAllFiles().ToList();
            Console.WriteLine("0. Exit");
            for (int i = 0; i < files.Count(); i++)
            {
                Console.WriteLine($"{i+1}. {files[i]}");
            }
            return files;
        }
    }
}