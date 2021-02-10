using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Position;

namespace Chess.ViewModels.BoardViewModel
{
    public class BoardViewModel
    {
        private readonly IBoard _board;

        public BoardViewModel(IBoard board)
        {
            _board = board;
        }

        public Figure GetFigureAtPosition(Position position)
        {
            return _board.GetFigureAtPosition(position);
        }

        public int GetBoardSize()
        {
            return _board.GetBoardSize();
        }

        public Position GetPositionAt(int x, int y)
        {
            return _board.GetPositionAt(x, y);
        }

        public IBoard GetCopy()
        {
            return _board.Copy();
        }
    }
}