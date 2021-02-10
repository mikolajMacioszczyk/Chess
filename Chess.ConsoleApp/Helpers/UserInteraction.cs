using System;
using System.Text.RegularExpressions;
using Chess.Enums;
using Chess.Models.Player;
using Chess.Models.Position;

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

        private static (string, string) GetUserNames()
        {
            Console.Write("User 1 name:\t");
            string firstUserName = ReadNotEmptyStringFromUser();
            Console.Write("User 2 name:\t");
            string secondUserName = ReadNotEmptyStringFromUser();
            return (firstUserName, secondUserName);
        }

        private static (TeamColor, TeamColor) GetTeamColors(string player1Name)
        {
            Console.WriteLine($"{player1Name} team: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" 1. White");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" 2. Black");
            Console.ForegroundColor = ConsoleColor.White;
            int choice = GetPositiveNumberFromUser("", "Expected positive number, please try again");

            if (choice == 1)
                return (TeamColor.White, TeamColor.Black);
            if (choice == 2)
                return (TeamColor.Black, TeamColor.White);

            Console.WriteLine($"Option {choice} not found. Please try again.");
            return GetTeamColors(player1Name);
        }
        
        public static (Player, Player) GetColorFromPlayer()
        {
            var names = GetUserNames();
            while (names.Item1.Equals(names.Item2))
            {
                Console.WriteLine("Users cannot have the same names");
                names = GetUserNames();
            }

            var colors = GetTeamColors(names.Item1);

            Console.WriteLine($"{names.Item1} has color: {colors.Item1}\n{names.Item2} has color: {colors.Item2}");
            return (new Player()
            {
                Name = names.Item1, TeamColor = colors.Item1
            }, new Player()
            {
                Name = names.Item2, TeamColor = colors.Item2
            });
        }
    }
}