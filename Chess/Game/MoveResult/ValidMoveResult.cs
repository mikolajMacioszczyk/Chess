using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveValidator;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Figures.FigureImplementation;
using Chess.Models.Position;
using Chess.ViewModels;

namespace Chess.Game.MoveResult
{
    [Serializable]
    public class ValidMoveResult : IMoveResult
    {
        private static readonly IsValidMoveResult IsValidMoveResult = new IsValidMoveResult(MoveResultStatus.Valid, string.Empty);
        private readonly IBoard _board;
        private readonly ICheckVerifier _verifier;
        private readonly IMoveValidator _moveValidator;
        private readonly LastMoveViewModel _lastMove;
        private readonly IEnumerable<Figure> _allSmashedFigures;
        
        public ValidMoveResult(
            IBoard board, 
            ICheckVerifier verifier,
            IMoveValidator moveValidator, 
            LastMoveViewModel lastMove, 
            IEnumerable<Figure> allSmashedFigures)
        {
            _board = board; 
            _verifier = verifier;
            _moveValidator = moveValidator;
            _lastMove = lastMove;
            _allSmashedFigures = allSmashedFigures;
        }

        public IsValidMoveResult IsValidMove()
        {
            return IsValidMoveResult;
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
            var validator = new OrdinaryBoardMoveValidator(copyBoard);
            var verifier = new OrdinaryBoardCheckVerifier(copyBoard, validator);
            foreach (var figureAndPos in FindPossibleBlockers(culprit, kingPosition, teamColor))
            {
                var startPosition = figureAndPos.Item1.Position;
                var blocker = copyBoard.RemoveFigure(startPosition);
                var figureAtEndPos = copyBoard.RemoveFigure(figureAndPos.Item2);
                blocker.Move(figureAndPos.Item2);
                copyBoard.SetFigure(blocker, figureAndPos.Item2);
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
                        var figure = _board.FigureAt(_board.GetPositionAt(i, j));
                        if (figure != null &&
                            figure.TeamColor == kingTeamColor &&
                            figure.FigureType != FigureType.King &&
                            figure.CanMove(field) && 
                            _moveValidator.CanMove(figure,field).Item1)
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
            var validator = new OrdinaryBoardMoveValidator(copyBoard);
            var verifier = new OrdinaryBoardCheckVerifier(copyBoard, validator);
            copyBoard.RemoveFigure(aimPosition);
            foreach (var possibleKiller in FindPossibleKillers(aimPosition, teamColor))
            {
                var startPosition = possibleKiller.Position;
                var killer = copyBoard.RemoveFigure(startPosition);
                killer.Move(aimPosition);
                copyBoard.SetFigure(killer, aimPosition);
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
                    var figure = _board.FigureAt(_board.GetPositionAt(j, i));
                    if (figure != null && 
                        figure.TeamColor == colorOfCheckedTeam && 
                        figure.CanMove(destinationPosition) && 
                        _moveValidator.CanMove(figure,destinationPosition).Item1)
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
            var validator = new OrdinaryBoardMoveValidator(copyBoard);
            var verifier = new OrdinaryBoardCheckVerifier(copyBoard, validator);
            foreach (var possibleKingMove in GetPossibleKingMoves(king))
            {
                King newKing = new King(possibleKingMove, king.TeamColor);
                copyBoard.RemoveFigure(king.Position);
                var figureAtKingMove = copyBoard.RemoveFigure(possibleKingMove);
                copyBoard.SetFigure(newKing, possibleKingMove);
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

            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row+1, king.Position.Column);
                
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row+1, king.Position.Column+1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row, king.Position.Column+1);

            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row+1, king.Position.Column-1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row, king.Position.Column-1);
            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row-1, king.Position.Column);
            
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row-1, king.Position.Column+1);
            AddPositionIfInBoundariesAndCanMove(output,king,king.Position.Row-1, king.Position.Column-1);

            return output;
        }

        private void AddPositionIfInBoundariesAndCanMove(List<Position> list, Figure figure, int posX, int posY)
        {
            if (posX >= 0 && posX < _board.GetBoardSize()
            && posY >= 0 && posY < _board.GetBoardSize())
            {
                var position = _board.GetPositionAt(posX, posY);
                if (_moveValidator.CanMove(figure,position).Item1)
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
            return (_lastMove.FigureMoved, _lastMove.From, _lastMove.Destination);
        }

        public bool IsLastMoveSmash()
        {
            return _lastMove.Smashed != null;
        }

        public Figure SmashedFigure()
        {
            return _lastMove.Smashed;
        }

        public IEnumerable<Figure> AllSmashedFigures()
        {
            return _allSmashedFigures;
        }

        public BoardViewModel GetBoard()
        {
            return new BoardViewModel(_board);
        }
    }
}