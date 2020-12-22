using System;
using Chess.Exceptions;
using Chess.Exceptions.InvalidBoardActionException;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;

namespace Chess.Game.CheckVerfier
{
    public class OrdinaryBoardCheckVerifier : ICheckVerifier
    {
        private IBoard _board;
        private IMoveValidator _moveValidator;
        private Figure _figureCausingCheck;
        private bool? _isCheck;
        private TeamColor _teamColor;

        public OrdinaryBoardCheckVerifier(IBoard board, IMoveValidator moveValidator)
        {
            _board = board;
            _moveValidator = moveValidator;
        }
        
        public void SetBoard(IBoard board)
        {
            _board = board ?? throw new NullReferenceException();
            SetState(null,null, TeamColor.None);
        }

        
        public bool IsCheck(TeamColor checkedTeam)
        {
            if (_isCheck.HasValue && _teamColor == checkedTeam)
            {
                return _isCheck.Value;
            }
            
            var king = FindKing(checkedTeam);

            Figure figureCausingCheck = FindFigureCausingCheck(king);
            if (figureCausingCheck != null)
            {
                SetState(true,figureCausingCheck, checkedTeam);
                return true;
            }
            SetState(false,null, TeamColor.None);
            return false;
        }

        private Figure FindFigureCausingCheck(Figure king)
        {
            for (int i = 0; i < OrdinaryChessBoard.BoardSize; i++)
            {
                for (int j = 0; j < OrdinaryChessBoard.BoardSize; j++)
                {
                    var figure = _board.FigureAt(_board.GetPositionAt(j, i));
                    if (figure != null 
                        && figure.TeamColor != king.TeamColor 
                        && figure.CanMove(king.Position) 
                        && _moveValidator.CanMove(figure, king.Position))
                    {
                        return figure;
                    }
                }
            }
            return null;
        }

        private void SetState(bool? isCheck, Figure figureCausingCheck, TeamColor teamColor)
        {
            _isCheck = isCheck;
            _figureCausingCheck = figureCausingCheck;
            _teamColor = teamColor;
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

        public Figure FigureCausingCheck(TeamColor teamColor)
        {
            if (_figureCausingCheck != null)
            {
                return _figureCausingCheck;
            }

            if (IsCheck(teamColor))
            {
                return _figureCausingCheck;
            }
            return null;
        }

        public bool VerifyMoveCauseCheck(Models.Position.Position @from, Models.Position.Position destination)
        {
            var copyBoard = _board.Copy();
            var figure = copyBoard.RemoveFigure(from);
            if (figure == null)
            {
                throw new RemoveFromBoardEmptyFieldException(from);
            }

            if (_moveValidator.CanMove(figure, destination))
            {
                figure.Move(destination);
                copyBoard.SetFigure(figure, destination);
            }
            else
            {
                throw new InvalidMoveException(from, destination, figure);
            }
            var verifier = new OrdinaryBoardCheckVerifier(copyBoard, new OrdinaryBoardMoveValidator(copyBoard));
            return verifier.IsCheck(TeamColor.Black) || verifier.IsCheck(TeamColor.White);
        }
    }
}