using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Exceptions;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Position;

namespace Chess.Game.MoveValidator
{
    [Serializable]
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
        public (bool, string) CanMove(Figure figure, Position position)
        {
            if (!VerifyPositionInBoundaries(position))
                return (false, "Position out of board");
            if (!VerifyPositionNotTakenByAlly(figure.TeamColor, position))
                return (false, "Position already taken by ally");
            if (!VerifyOtherFiguresNotBlockMove(figure, position))
                return (false, "Other figures are on the path to the target position");
            if (!VerifyPawnMove(figure, position))
                return (false, "Invalid move for figure type: Pawn");
            return (true, "OK");
        }

        public void Update(IBoard board)
        {
            _board = board;
        }

        public bool VerifyPositionInBoundaries(Position position)
        {
            if (position.Row < 0 || 
                position.Column < 0 || 
                position.Row >= OrdinaryChessBoard.BoardSize ||
                position.Column >= OrdinaryChessBoard.BoardSize)
            {
                return false;
            }
            return true;
        }

        private bool VerifyPositionNotTakenByAlly(TeamColor myTeamColor, Position destinationPosition)
        {
            var figure = _board.FigureAt(destinationPosition);
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
            var figureAtPosition = _board.FigureAt(position);

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