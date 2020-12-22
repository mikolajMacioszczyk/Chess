using System;
using System.Collections.Generic;
using Chess.Exceptions;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Figures.FigureImplementation;
using Chess.Models.Position;

// Tests!!!

namespace Chess.Game.MoveResult
{
    public class MoveResult : IMoveResult
    {
        private readonly IBoard _board;
        private readonly IMoveValidator _moveValidator;
        private readonly Models.Position.Position _lastMovePosisionFrom;
        private readonly Models.Position.Position _lastMovePosisionDest;
        private readonly Figure _lastMoveFigureMoved;
        private readonly Figure _lastMoveFigureSmashed;
        
        public MoveResult(
            IBoard board, 
            Figure lastMoveFigureMoved, 
            Models.Position.Position lastMovePosisionFrom, 
            Models.Position.Position lastMovePosisionDest,
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
            if (IsCheckMate(TeamColor.Black))
            {
                return TeamColor.Black;
            }
            if (IsCheckMate(TeamColor.White))
            {
                return TeamColor.White;
            }

            return TeamColor.None;
        }   

        /// <summary>
        /// Verify one by one figure, if can kill king 
        /// </summary>
        /// <param name="myTeamColor">My team color</param>
        /// <returns></returns>
        public (bool, Figure) IsCheck(TeamColor myTeamColor)
        {
            TeamColor checkedTeamColor;
            switch (myTeamColor)
            {
                case TeamColor.White:
                    checkedTeamColor = TeamColor.Black;
                    break;
                case TeamColor.Black:
                    checkedTeamColor = TeamColor.White;
                    break;
                case TeamColor.None:
                    return (false, null);
                default:
                    throw new ArgumentOutOfRangeException(nameof(myTeamColor), myTeamColor, null);
            }
            
            var king = FindKing(checkedTeamColor);

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

        private Figure FindKing(TeamColor kingTeamColor)
        {
            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                    if (figure != null && figure.FigureType == FigureType.King && figure.TeamColor == kingTeamColor)
                    {
                        return figure;
                    }
                }
            }
            throw new ImplementationException("King not found");
        }

        /// <summary>
        /// Verify if game is not done
        /// </summary>
        /// <param name="myTeamColor">My team color</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsCheckMate(TeamColor myTeamColor)
        {
            TeamColor checkedTeamColor;
            switch (myTeamColor)
            {
                case TeamColor.White:
                    checkedTeamColor = TeamColor.Black;
                    break;
                case TeamColor.Black:
                    checkedTeamColor = TeamColor.White;
                    break;
                case TeamColor.None:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(myTeamColor), myTeamColor, null);
            }
            var king = FindKing(checkedTeamColor);
            var check = IsCheck(myTeamColor);
            if (!check.Item1)
            {
                return false;
            }

            if (CheckIfKingMayEscape(king, checkedTeamColor) || CheckIfAfterKillKingWillBeFree(check.Item2.Position, checkedTeamColor))
            {
                return false;
            }

            throw new NotImplementedException();
            // another figure may block the way
        }

        private IEnumerable<(Figure, Models.Position.Position)> FindPossibleBlockers(Figure culprit, Models.Position.Position kingPosition, TeamColor teamColor)
        {
            if (culprit.FigureType ==  FigureType.Knight)
            {
                return new List<(Figure, Models.Position.Position)>();
            }

            throw new NotImplementedException();
        }

        private IEnumerable<Models.Position.Position> GetPathFromTo(Models.Position.Position from, Models.Position.Position to)
        {
            var vector = new Vector(from, to);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Helper function to IsCheckMate
        /// Verify if after killing there is not Check
        /// </summary>
        /// <param name="aimPosition">Position of figure which cause Check</param>
        /// <param name="teamColor">Color of checked Team</param>
        /// <returns></returns>
        private bool CheckIfAfterKillKingWillBeFree(Models.Position.Position aimPosition, TeamColor teamColor)
        {
            foreach (var possibleKiller in FindPossibleKillers(aimPosition, teamColor))
            {
                IBoard newBoard = _board.Copy();
                newBoard.RemoveFigure(aimPosition);
                newBoard.RemoveFigure(possibleKiller.Position);
                Figure killer = new Pawn(aimPosition, teamColor);
                newBoard.SetFigure(killer, aimPosition);
                var newMoveResult = new MoveResult(newBoard, killer, possibleKiller.Position, aimPosition, null, _moveValidator);
                if (!newMoveResult.IsCheck(teamColor).Item1)
                {
                    return true;
                }
            }
            return false;
        }
        
        private IEnumerable<Figure> FindPossibleKillers(Models.Position.Position destinationPosition,
            TeamColor colorOfCheckedTeam)
        {
            List<Figure> killers = new List<Figure>(); 
            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                    if (figure != null && figure.TeamColor == colorOfCheckedTeam && figure.CanMove(destinationPosition) && _moveValidator.CanMove(figure,destinationPosition))
                    {
                        killers.Add(figure);
                    }
                }
            }
            return killers;
        }
        
        /// <summary>
        /// Helper function to IsCheckMate
        /// Verify if Any of king possible move is not Check
        /// </summary>
        /// <param name="king"></param>
        /// <param name="teamColor"></param>
        /// <returns></returns>
        private bool CheckIfKingMayEscape(Figure king, TeamColor teamColor)
        {
            foreach (var possibleKingMove in GetPossibleKingMoves(king))
            {
                IBoard newBoard = _board.Copy();
                newBoard.RemoveFigure(king.Position);
                King newKing = new King(possibleKingMove, king.TeamColor);
                newBoard.SetFigure(newKing, possibleKingMove);
                var newMoveResult = new MoveResult(newBoard, newKing, king.Position, possibleKingMove, null, _moveValidator);
                if (!newMoveResult.IsCheck(teamColor).Item1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Helper function to IsCheck Mate
        /// Returns All Moves King may perform based on current board state
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private IEnumerable<Models.Position.Position> GetPossibleKingMoves(Figure king)
        {
            List<Models.Position.Position> output = new List<Models.Position.Position>();
            Models.Position.Position newKingPosition;

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

        public (Figure, Models.Position.Position,Models.Position.Position) LastMoveFigureAndPositionFromAndDest()
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