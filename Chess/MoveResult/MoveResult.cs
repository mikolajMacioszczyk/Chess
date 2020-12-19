using System;
using System.Collections.Generic;
using Chess.Board;
using Chess.Exceptions;
using Chess.Figures;
using Chess.Figures.FigureImplementation;
using Chess.MoveValidator;
using Chess.Team;

namespace Chess.MoveResult
{
    public class MoveResult : IMoveResult
    {
        private readonly IBoard _board;
        private readonly IMoveValidator _moveValidator;
        private readonly Position.Position _lastMovePosisionFrom;
        private readonly Position.Position _lastMovePosisionDest;
        private readonly Figure _lastMoveFigureMoved;
        private readonly Figure _lastMoveFigureSmashed;
        
        public MoveResult(
            IBoard board, 
            Figure lastMoveFigureMoved, 
            Position.Position lastMovePosisionFrom, 
            Position.Position lastMovePosisionDest,
            Figure lastMoveFigureSmashed,
            IMoveValidator moveValidator)
        {
            this._board = board;
            this._lastMoveFigureMoved = lastMoveFigureMoved;
            this._lastMovePosisionFrom = lastMovePosisionFrom;
            this._lastMovePosisionDest = lastMovePosisionDest;
            this._lastMoveFigureSmashed = lastMoveFigureSmashed;
            this._moveValidator = moveValidator;
        }

        public TeamColor Winner()
        {
            throw new System.NotImplementedException();
        }

        public (bool, Figure) IsCheck(TeamColor teamColor)
        {
            var king = FindKing(teamColor);

            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                    if (figure != null && figure.CanMove(king.Position) && _moveValidator.CanMove(figure, king.Position))
                    {
                        return (true, figure);
                    }
                }
            }
            return (false, null);
        }

        private Figure FindKing(TeamColor teamColor)
        {
            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                    if (figure != null && figure.FigureType == FigureType.King && figure.TeamColor == teamColor)
                    {
                        return figure;
                    }
                }
            }
            throw new ImplementationException("King not found");
        }

        public bool IsCheckMate(TeamColor teamColor)
        {
            var king = FindKing(teamColor);
            var isCheck = IsCheck(teamColor);
            if (!isCheck.Item1)
            {
                return false;
            }

            if (king.Position.PositionX + 1 < OrdinaryChessBoard.BoardSize)
            {
                var newKingPosition = _board.GetPositionAt(king.Position.PositionX + 1, king.Position.PositionY);
                if (_moveValidator.CanMove(king, newKingPosition))
                {
                    IBoard newBoard = _board.Copy();
                    newBoard.RemoveFigure(king.Position);
                    King newKing = new King(newKingPosition, king.TeamColor);
                    newBoard.SetFigure(newKing, newKingPosition);
                }
            }
        }

        private List<Position.Position> GetPossibleKingMoves(Figure king)
        {
            List<Position.Position> output = new List<Position.Position>();
            Position.Position newKingPosition;

            if (king.Position.PositionX + 1 < OrdinaryChessBoard.BoardSize)
            {
                newKingPosition = _board.GetPositionAt(king.Position.PositionX + 1, king.Position.PositionY);
                if (_moveValidator.CanMove(king, newKingPosition))
                {
                    output.Add(newKingPosition);
                }

                if (king.Position.PositionY + 1 < OrdinaryChessBoard.BoardSize)
                {
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX + 1, king.Position.PositionY + 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                    
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX, king.Position.PositionY + 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                }
            
                if (king.Position.PositionY - 1 >= 0)
                {
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX + 1, king.Position.PositionY - 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                    
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX, king.Position.PositionY - 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                }
            }
            
            if (king.Position.PositionX - 1 >= 0)
            {
                newKingPosition = _board.GetPositionAt(king.Position.PositionX - 1, king.Position.PositionY);
                if (_moveValidator.CanMove(king, newKingPosition))
                {
                    output.Add(newKingPosition);
                }

                if (king.Position.PositionY + 1 < OrdinaryChessBoard.BoardSize)
                {
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX - 1, king.Position.PositionY + 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                }
            
                if (king.Position.PositionY - 1 >= 0)
                {
                    newKingPosition = _board.GetPositionAt(king.Position.PositionX - 1, king.Position.PositionY - 1);
                    if (_moveValidator.CanMove(king, newKingPosition))
                    {
                        output.Add(newKingPosition);
                    }
                }
            }

            return output;
        }

        public int GetScore(TeamColor teamColor)
        {
            return _board.GetScoreForTeam(teamColor);
        }

        public (Figure, Position.Position,Position.Position) LastMoveFigureAndPositionFromAndDest()
        {
            return (_lastMoveFigureMoved, _lastMovePosisionFrom, _lastMovePosisionDest);
        }

        public bool IsLastMoveSmash()
        {
            return _lastMoveFigureSmashed != null;
        }

        public Figure SmashedFigure()
        {
            return _lastMoveFigureSmashed;
        }
    }
}