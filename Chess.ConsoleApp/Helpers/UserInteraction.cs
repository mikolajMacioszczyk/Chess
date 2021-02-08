using System;
using System.Text.RegularExpressions;
using Chess.ConsoleApp.Enums;
using Chess.Models.Position;

namespace Chess.ConsoleApp.Helpers
{
    public static class UserInteraction
    {
        public static int GetPositiveNumberFromUser(string message, string callback)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                return number;
            }
            
            Console.WriteLine(callback);
            return GetPositiveNumberFromUser(message, callback);
        }

        public static string GetStringFromUser(string message, string patter, string callback)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            Regex rg = new Regex(patter);
            if (rg.IsMatch(input ?? string.Empty))
            {
                return input;
            }
            
            Console.WriteLine(callback);
            return GetStringFromUser(message, patter, callback);
        }

        private static bool ParseStringToIntCoordinate(string coordinate, out int num)
        {
            Console.Write(coordinate);
            string input = Console.ReadLine();
            if (!int.TryParse(input, out num))
            {
                Console.WriteLine("Expected integer number. Try again");
                return false;
            }
            return true;
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

            return new Position(x, y);
        }
    }
}