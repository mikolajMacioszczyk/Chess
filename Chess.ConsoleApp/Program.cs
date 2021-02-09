using System;
using Chess.ConsoleApp.Application;
using Chess.ConsoleApp.Helpers;
using Chess.Models.Board;
using Chess.ViewModels.BoardViewModel;

namespace Chess.ConsoleApp
{
    class Program
    {
        private static string _introText1 =
            "\nWelcome in Chess Console Game.\nYou will be able to make movements through the following steps:\n\t1. Selecting your figure by entering its position, first row, then column.\n" +
            "\t2. Selecting the position to which you want to move your figure, first row, then column\nNote that you can enter both numbers and letters as coordinates\n\n" +
            "The game will check if the move for the given figure is possible\nIf it is, the move will be executed. Otherwise, you'll be asked to repeat steps 1 and 2\n\nStart board appearance:\n";
        private static string _introText2 = "\nThe BLACK team's figures are displayed in this color";
        private static string _introText3 = "The WHITE team's figures are displayed in this color";
        private static string _introText4 =
            "\nExample of movement execution:\nLet's assume that we have white pawns and we want to move the left horse to the left edge of the board.\n" +
            "So we want to make a move from position [1, 2] to position [3, 1]\nThe computer will first ask you for the position of the figure you want to move:\n" +
            "We enter the x-coordinate:\n\tX: ";
        private static string _introText5 = "Next the y-coordinate:\n\tY: ";
        private static string _introText6 = "Next the computer will first ask you for the destination position:\nWe enter the x-coordinate:\n\tX: ";
        private static string _introText7 = "Next the y-coordinate:\n\tY: ";
        private static string _introText8 = "The move for this figure is correct, so it is executed\nThe board now looks like:";
        private static string _introText9 = "Figures:\nK - King\nQ - Queen\nB - Bishop\nN - Knight\nR - Rook\np - Pawn\nx - empty field";
        private static string _introText10 = "Your turn. Enjoy the game!";
        static void ShowIntroductionMenu()
        {
            Console.WriteLine(" ======================== Welcome ========================");
            Console.WriteLine(" 1. New Game");
            Console.WriteLine(" 2. Read Game");
            Console.WriteLine(" 3. Quit");
        }

        private static void ShowInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void ShowInstructions()
        {
            Console.WriteLine(_introText1);
            var board = new OrdinaryChessBoard();
            BoardDisplay.ShowBoard(new BoardViewModel(board));
            ShowInColor(_introText2, ConsoleColor.Red);
            ShowInColor(_introText3, ConsoleColor.Blue);
            Console.Write(_introText4);
            ShowInColor("1", ConsoleColor.Green);
            Console.Write(_introText5);
            ShowInColor("2", ConsoleColor.Green);
            Console.Write(_introText6);
            ShowInColor("3", ConsoleColor.Green);
            Console.Write(_introText7);
            ShowInColor("1", ConsoleColor.Green);
            Console.WriteLine(_introText8);
            
            var knight = board.RemoveFigure(board.GetPositionAt(0, 1));
            board.SetFigure(knight, board.GetPositionAt(2,0));
            BoardDisplay.ShowBoard(new BoardViewModel(board));
            Console.WriteLine(_introText9);
            Console.WriteLine(_introText10);
        }
        
        static void Main(string[] args)
        {
            ShowInstructions();
            Console.WriteLine();
            
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