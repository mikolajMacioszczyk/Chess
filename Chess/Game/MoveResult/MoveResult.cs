using System;
using System.Collections.Generic;
using Chess.Exceptions;
using Chess.Game.CheckVerfier;
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
        private readonly ICheckVerifier _verifier;
        private readonly IMoveValidator _moveValidator;
        private readonly Position _lastMovePosisionFrom;
        private readonly Position _lastMovePosisionDest;
        private readonly Figure _lastMoveFigureMoved;
        private readonly Figure _lastMoveFigureSmashed;
        
        public MoveResult(
            IBoard board, 
            ICheckVerifier verifier,
            Figure lastMoveFigureMoved,
            Position lastMovePosisionFrom, 
            Position lastMovePosisionDest,
            Figure lastMoveFigureSmashed,
            IMoveValidator moveValidator)
        {
            _board = board;
            _verifier = verifier;
            _lastMoveFigureMoved = lastMoveFigureMoved;
            _lastMovePosisionFrom = lastMovePosisionFrom;
            _lastMovePosisionDest = lastMovePosisionDest;
            _lastMoveFigureSmashed = lastMoveFigureSmashed;
            _moveValidator = moveValidator;
        }

        /// <summary>
        /// Verify one by one figure, if can kill king 
        /// </summary>
        /// <param name="myTeamColor">My team color</param>
        /// <returns></returns>
        public bool IsCheck(TeamColor myTeamColor)
        {
            if (_verifier.IsCheck(myTeamColor))
            {
                return true;
            }
            return false;
        }

        public Figure FigureCausingCheck(TeamColor teamColor)
        {
            return _verifier.FigureCausingCheck(teamColor);
        }

        /// <summary>
        /// Verify if game is not done
        /// </summary>
        /// <param name="myTeamColor">My team color</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsCheckMate(TeamColor myTeamColor)
        {
            var king = _board.GetKing(myTeamColor);
            if (!IsCheck(myTeamColor))
            {
                return false;
            }

            if (!IsCheck(myTeamColor) ||
                VerifyIfKingMayEscape(king, myTeamColor) || 
                VerifyIfAfterKillKingWillBeFree(_verifier.FigureCausingCheck(myTeamColor).Position, myTeamColor) ||
                VerifyIfOtherFigureMayBlock(_verifier.FigureCausingCheck(myTeamColor), king.Position, myTeamColor))
            {
                return false;
            }
            return true;
        }

        private bool VerifyIfOtherFigureMayBlock(Figure culprit, Position kingPosition,TeamColor teamColor)
        {
            IBoard copyBoard = _board.Copy();
            foreach (var figureAndPos in FindPossibleBlockers(culprit, kingPosition, teamColor))
            {
                var startPosition = figureAndPos.Item1.Position;
                var blocker = copyBoard.RemoveFigure(startPosition);
                var figureAtEndPos = copyBoard.RemoveFigure(figureAndPos.Item2);
                blocker.Move(figureAndPos.Item2);
                copyBoard.SetFigure(blocker, figureAndPos.Item2);
                var verifier = new OrdinaryBoardCheckVerifier(copyBoard, new OrdinaryBoardMoveValidator(_board));
                if (!verifier.IsCheck(teamColor))
                {
                    blocker.Move(startPosition);
                    return true;
                }
                blocker.Move(startPosition);
                copyBoard.SetFigure(blocker,startPosition);
                copyBoard.SetFigure(figureAtEndPos, figureAndPos.Item2);
            }
            return false;
        }

        private IEnumerable<(Figure, Position)> FindPossibleBlockers(Figure culprit, Position kingPosition, TeamColor kingTeamColor)
        {
            if (culprit.FigureType ==  FigureType.Knight)
            {
                return new List<(Figure, Position)>();
            }

            var path = new Vector(culprit.Position, kingPosition).GetPath();
            var blockers = new List<(Figure, Position)>();

            foreach (var field in path)
            {
                for (int i = 0; i < _board.GetBoardSize(); i++)
                {
                    for (int j = 0; j < _board.GetBoardSize(); j++)
                    {
                        var figure = _board.GetFigureAtPosition(_board.GetPositionAt(i, j));
                        if (figure != null &&
                            figure.TeamColor == kingTeamColor &&
                            figure.FigureType != FigureType.King &&
                            figure.CanMove(field) && 
                            _moveValidator.CanMove(figure,field))
                        {
                            blockers.Add((figure, field));
                        }
                    }
                }
            }
            return blockers;
        }

        /// <summary>
        /// Helper function to IsCheckMate
        /// Verify if after killing there is not Check
        /// </summary>
        /// <param name="aimPosition">Position of figure which cause Check</param>
        /// <param name="teamColor">Color of checked Team</param>
        /// <returns></returns>
        private bool VerifyIfAfterKillKingWillBeFree(Position aimPosition, TeamColor teamColor)
        {
            IBoard copyBoard = _board.Copy();
            copyBoard.RemoveFigure(aimPosition);
            foreach (var possibleKiller in FindPossibleKillers(aimPosition, teamColor))
            {
                var startPosition = possibleKiller.Position;
                var killer = copyBoard.RemoveFigure(startPosition);
                killer.Move(aimPosition);
                copyBoard.SetFigure(killer, aimPosition);
                var verifier = new OrdinaryBoardCheckVerifier(copyBoard, new OrdinaryBoardMoveValidator(_board));
                if (!verifier.IsCheck(teamColor))
                {
                    killer.Move(startPosition);
                    return true;
                }
                killer.Move(startPosition);
                copyBoard.SetFigure(killer,startPosition);
            }
            return false;
        }
        
        private IEnumerable<Figure> FindPossibleKillers(Position destinationPosition, TeamColor colorOfCheckedTeam)
        {
            List<Figure> killers = new List<Figure>(); 
            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                    if (figure != null && 
                        figure.TeamColor == colorOfCheckedTeam && 
                        figure.CanMove(destinationPosition) && 
                        _moveValidator.CanMove(figure,destinationPosition))
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
        private bool VerifyIfKingMayEscape(Figure king, TeamColor teamColor)
        {
            IBoard copyBoard = _board.Copy();
            foreach (var possibleKingMove in GetPossibleKingMoves(king))
            {
                King newKing = new King(possibleKingMove, king.TeamColor);
                var figureAtKingMove = copyBoard.RemoveFigure(possibleKingMove);
                copyBoard.SetFigure(newKing, possibleKingMove);
                var verifier = new OrdinaryBoardCheckVerifier(copyBoard, new OrdinaryBoardMoveValidator(copyBoard));
                if (!verifier.IsCheck(teamColor))
                {
                    return true;
                }
                copyBoard.SetFigure(figureAtKingMove,possibleKingMove);
                copyBoard.SetFigure(king, king.Position);
            }
            return false;
        }

        /// <summary>
        /// Helper function to IsCheck Mate
        /// Returns All Moves King may perform based on current board state
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private IEnumerable<Position> GetPossibleKingMoves(Figure king)
        {
            List<Position> output = new List<Position>();

            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX+1, king.Position.PositionY);
                
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX+1, king.Position.PositionY+1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX, king.Position.PositionY+1);

            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX+1, king.Position.PositionY-1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX, king.Position.PositionY-1);
            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX-1, king.Position.PositionY);
            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX-1, king.Position.PositionY+1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.PositionX-1, king.Position.PositionY-1);

            return output;
        }

        private void AddPositionIfInBoundariesAndCanMove(List<Position> list, Figure figure, int posX, int posY)
        {
            if (posX >= 0 && posX < _board.GetBoardSize()
            && posY >= 0 && posY < _board.GetBoardSize())
            {
                var position = _board.GetPositionAt(posX, posY);
                if (_moveValidator.CanMove(figure,position))
                {
                    list.Add(position);
                }
            }
        }
        
        public int GetScore(TeamColor teamColor)
        {
            return _board.GetScoreForTeam(teamColor);
        }

        public (Figure, Position,Position) LastMoveFigureAndPositionFromAndDest()
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