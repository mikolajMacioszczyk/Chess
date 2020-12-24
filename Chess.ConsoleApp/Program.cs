using Chess.Models.Board;
using Chess.ViewModels.BoardViewModel;

namespace Chess.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new OrdinaryChessBoard();
            ConsoleInteraction.ShowBoard(new BoardViewModel(board));
        }
    }
}