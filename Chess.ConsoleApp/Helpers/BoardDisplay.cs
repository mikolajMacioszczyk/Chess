using System;
using Chess.Game.Team;
using Chess.Models.Figures;
using Chess.ViewModels.BoardViewModel;

namespace Chess.ConsoleApp.Helpers
{
    public static class BoardDisplay
    {
        public static void ShowBoard(BoardViewModel board)
        {
            Console.WriteLine();
            ShowCharacterCoordinate(board);
            Console.WriteLine();
            string delimeter = "\t";
            for (int i = board.GetBoardSize() - 1; i >= 0; i--)
            {
                Console.Write(i + 1 +delimeter);
                for (int j = 0; j < board.GetBoardSize(); j++)
                {
                    var figure = board.GetFigureAtPosition(board.GetPositionAt(i,j));
                    if (figure == null)
                    {
                        Console.Write('x'+delimeter);
                    }
                    else
                    {
                        ShowFigure(figure);
                    }
                }
                Console.WriteLine(i);
            }
            Console.WriteLine();
            ShowCharacterCoordinate(board);
        }

        private static void ShowCharacterCoordinate(BoardViewModel board)
        {
            Console.Write("\t");
            for (char c = 'a'; c < 'a' + board.GetBoardSize(); c++)
            {
                Console.Write(c+"\t");
            }
            Console.WriteLine();
        }

        private static void ShowFigure(Figure figure)
        {
            switch (figure.TeamColor)
            {
                case TeamColor.White:
                    ShowWhiteFigure(figure.FigureType);
                    break;
                case TeamColor.Black:
                    ShowBlackFigure(figure.FigureType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ShowBlackFigure(FigureType figureType)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string delimeter = "\t";
            ShowFigure(figureType, delimeter);
            Console.ResetColor();
        }
        
        private static void ShowWhiteFigure(FigureType figureType)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string delimeter = "\t";
            ShowFigure(figureType, delimeter);
            Console.ResetColor();
        }

        private static void ShowFigure(FigureType figureType, string delimeter)
        {
            switch (figureType)
            {
                case FigureType.King:
                    Console.Write('K'+delimeter);
                    break;
                case FigureType.Queen:
                    Console.Write('Q'+delimeter);
                    break;
                case FigureType.Rook:
                    Console.Write('R'+delimeter);
                    break;
                case FigureType.Bishop:
                    Console.Write('B'+delimeter);
                    break;
                case FigureType.Knight:
                    Console.Write('N'+delimeter);
                    break;
                case FigureType.Pawn:
                    Console.Write('p'+delimeter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }
    }
}