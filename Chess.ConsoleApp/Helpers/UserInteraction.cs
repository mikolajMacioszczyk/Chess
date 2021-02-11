using System;
using System.Text.RegularExpressions;
using Chess.Enums;
using Chess.Models.Player;
using Chess.Models.Position;
using PlayerComputerAI.AI;

namespace Chess.ConsoleApp.Helpers
{
    public static class UserInteraction
    {
        public static int GetPositiveNumberFromUser(string message, string callback)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int number) && number >= 0)
            {
                return number;
            }
            
            Console.WriteLine(callback);
            return GetPositiveNumberFromUser(message, callback);
        }

        private static bool ParseStringToIntCoordinate(string coordinate, out int num)
        {
            Console.Write(coordinate);
            string input = Console.ReadLine();
            
            if (input?.Trim().Length == 1 && char.IsLetter(input[0]))
            {
                num = input[0] - 'a' + 1;
                return true;
            }
            
            if (int.TryParse(input, out num))
            {
                return true;
            }
            
            Console.WriteLine("Expected integer number. Try again");
            return false;
        }

        public static string ReadNotEmptyStringFromUser()
        {
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please type not empty string");
                input = Console.ReadLine();
            }
            return input;
        }
        
        public static Position GetPositionFromUser(string message)
        {
            Console.WriteLine(message);
            if (!ParseStringToIntCoordinate("X: ", out int x))
            {
                return GetPositionFromUser(message);
            }

            if (!ParseStringToIntCoordinate("Y: ", out int y))
            {
                return GetPositionFromUser(message);
            }

            return new Position(x-1, y-1);
        }
        
        private static string GetPlayerName(int number)
        {
            Console.Write($"User {number} name:\t");
            return ReadNotEmptyStringFromUser();
        }

        private static TeamColor GetTeamColorFromPlayer(string name)
        {
            Console.WriteLine($"{name} team: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" 1. White");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" 2. Black");
            Console.ForegroundColor = ConsoleColor.White;
            int choice = GetPositiveNumberFromUser("", "Expected positive number, please try again");

            if (choice == 1)
                return TeamColor.White;
            if (choice == 2)
                return TeamColor.Black;
            
            Console.WriteLine($"Option {choice} not found. Please try again.");
            return GetTeamColorFromPlayer(name);
        }

        public static Player GetPlayerFromUser()
        {
            var name = GetPlayerName(1);
            var color = GetTeamColorFromPlayer(name);

            return new Player(name, color);
        }

        private static (string, string) GetUserNames()
        {
            return (GetPlayerName(1), GetPlayerName(2));
        }

        private static (TeamColor, TeamColor) GetTeamColorsForPlayer(string player1Name)
        {
            TeamColor fromPlayer = GetTeamColorFromPlayer(player1Name);
            if (fromPlayer == TeamColor.Black)
            {
                return (TeamColor.Black, TeamColor.White);
            }

            return (TeamColor.White, TeamColor.Black);
        }
        
        public static (Player, Player) GetColorFromTwoPlayers()
        {
            var names = GetUserNames();
            while (names.Item1.Equals(names.Item2))
            {
                Console.WriteLine("Users cannot have the same names");
                names = GetUserNames();
            }

            var colors = GetTeamColorsForPlayer(names.Item1);

            Console.WriteLine($"{names.Item1} has color: {colors.Item1}\n{names.Item2} has color: {colors.Item2}");
            return (new Player(names.Item1, colors.Item1), new Player(names.Item2, colors.Item2));
        }

        public static DifficultyLevel GetDifficultyLevelFromUser()
        {
            Console.WriteLine("Select difficulty level:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. High");
            Console.WriteLine("4. Expert");
            int choice = GetPositiveNumberFromUser(
                "", "Expected positive number. Try again");
            switch (choice)
            {
                case 1:
                    return DifficultyLevel.Low;
                case 2:
                    return DifficultyLevel.Normal;
                case 3:
                    return DifficultyLevel.High;
                case 4:
                    return DifficultyLevel.Expert;
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again.");
                    return GetDifficultyLevelFromUser();
            }
        }
    }
}