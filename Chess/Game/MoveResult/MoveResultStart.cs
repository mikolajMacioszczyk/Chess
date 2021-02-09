using Chess.Enums;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Position;
using Chess.ViewModels.BoardViewModel;

namespace Chess.Game.MoveResult
{
    public class MoveResultStart : IMoveResult
    {
        private static readonly IsValidMoveResult ValidMoveResult = new IsValidMoveResult(MoveResultStatus.Valid, string.Empty);
        public IsValidMoveResult IsValidMove()
        {
            return ValidMoveResult;
        }

        public bool IsCheck(TeamColor teamColor)
        {
            return false;
        }

        public Figure FigureCausingCheck(TeamColor teamColor)
        {
            return null;
        }

        public bool IsCheckMate(TeamColor teamColor)
        {
            return false;
        }

        public int GetScore(TeamColor teamColor)
        {
            return 0;
        }

        public (Figure, Position, Position) LastMoveFigureAndPositionFromAndDest()
        {
            return (null, null, null);
        }

        public bool IsLastMoveSmash()
        {
            return false;
        }

        public Figure SmashedFigure()
        {
            return null;
        }

        public BoardViewModel GetBoard()
        {
            return new BoardViewModel(new OrdinaryChessBoard());
        }
    }
}