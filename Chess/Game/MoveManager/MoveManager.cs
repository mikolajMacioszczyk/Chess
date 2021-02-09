using System;
using Chess.Exceptions.InvalidBoardActionException;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Position;
using Chess.ViewModels.LastMoveViewModel;

namespace Chess.Game.MoveManager
{
    public class MoveManager : IMoveManager
    {
        private readonly IMoveValidator _moveValidator;
        private readonly ICheckVerifier _verifier;
        private IBoard _board;

        public MoveManager()
        {
            _board = new OrdinaryChessBoard();
            _moveValidator = new OrdinaryBoardMoveValidator(_board);
            _verifier = new OrdinaryBoardCheckVerifier(_board, _moveValidator);
        }

        public bool CanMove(Position from, Position destination)
        {
            var figure = _board.GetFigureAtPosition(from);
            if (figure == null)
            {
                throw new NullReferenceException();
            }
            if (!figure.CanMove(destination) || !_moveValidator.CanMove(figure,destination))
            {
                return false;
            }

            return !_verifier.VerifyMoveCauseCheck(figure.Position, destination);
        }

        public IMoveResult Move(Position @from, Position destination)
        {
            var figure = _board.GetFigureAtPosition(from);
            _board.RemoveFigure(from);
            var killed = _board.RemoveFigure(destination);
            figure.Move(destination);
            _board.SetFigure(figure, destination);
            var lastMoveViewModel = new LastMoveViewModel(figure, from, destination, killed);
            return new ValidMoveResult(_board, _verifier, _moveValidator, lastMoveViewModel);
        }

        public bool IsEnemyAtPosition(Position position, TeamColor myTeamColor)
        {
            var figure = _board.GetFigureAtPosition(position);
            return figure != null && figure.TeamColor != myTeamColor;
        }

        public bool IsAllyAtPosition(Position position, TeamColor myTeamColor)
        {
            var figure = _board.GetFigureAtPosition(position);
            return figure != null && figure.TeamColor == myTeamColor;
        }

        public bool IsPositionFree(Position position)
        {
            return _board.GetFigureAtPosition(position) == null;
        }
    }
}