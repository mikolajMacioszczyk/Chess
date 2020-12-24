using System.Collections.Generic;
using Chess.Exceptions;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Position;

namespace Chess.Game.MoveValidator
{
    public class OrdinaryBoardMoveValidator : IMoveValidator
    {
        private IBoard _board;

        public OrdinaryBoardMoveValidator(IBoard board)
        {
            _board = board;
        }
        /// <summary>
        /// Expects Move is Legal for this type of Figure
        /// Check if position is not out of board
        /// Check if position is not taken by ally
        /// Check if other figure not block move
        /// Does not verify if there is Check after move
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool CanMove(Figure figure, Position position)
        {
            return VerifyPositionInBoundaries(position) &&
                   VerifyPositionNotTakenByAlly(figure.TeamColor, position) &&
                   VerifyOtherFiguresNotBlockMove(figure, position) &&
                   VerifyPawnMove(figure, position);
        }

        public void Update(IBoard board)
        {
            _board = board;
        }

        private bool VerifyPositionInBoundaries(Position position)
        {
            if (position.PositionX < 0 || 
                position.PositionY < 0 || 
                position.PositionX >= OrdinaryChessBoard.BoardSize ||
                position.PositionY >= OrdinaryChessBoard.BoardSize)
            {
                return false;
            }
            return true;
        }

        private bool VerifyPositionNotTakenByAlly(TeamColor myTeamColor, Position destinationPosition)
        {
            var figure = _board.GetFigureAtPosition(destinationPosition);
            return figure == null || figure.TeamColor != myTeamColor;
        }
        
        private bool VerifyOtherFiguresNotBlockMove(Figure figure, Position destinationPosition)
        {
            if (figure.FigureType == FigureType.Knight)
            {
                return true;
            }
            var vector = new Vector(figure.Position, destinationPosition);
            IEnumerable<Position> path = vector.GetPath();
            foreach (var position in path)
            {
                if (_board.FigureAt(position) != null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool VerifyPawnMove(Figure figure, Position position)
        {
            if (figure.FigureType != FigureType.Pawn)
            {
                return true;
            }

            var vector = new Vector(figure.Position, position);
            var figureAtPosition = _board.GetFigureAtPosition(position);

            if (vector.IsVertical)
            {
                return figureAtPosition == null;
            }
            if (vector.IsDiagonal)
            {
                return figureAtPosition != null && figureAtPosition.TeamColor != figure.TeamColor;
            }
            throw new ImplementationException("Pawn move not Vertical not Diagonal");
        }
    }
}