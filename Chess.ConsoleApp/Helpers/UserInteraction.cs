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

        private static bool ParseStringToIntCoordinate(string coordinate, out int num)
        {
            Console.Write(coordinate);
            string input = Console.ReadLine();
            
            if (input?.Trim().Length == 1 && char.IsLetter(input[0]))
            {
                num = Math.Abs(input[0] - 'b');
                return true;
            }
            
            if (int.TryParse(input, out num))
            {
                return true;
            }
            
            Console.WriteLine("Expected integer number. Try again");
            return false;
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