using System;
using Chess.Enums;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.Models.Position;
using Chess.ViewModels.LastMoveViewModel;

namespace Chess.Game.MoveManager
{
    public class MoveManager : IMoveManager
    {
        private readonly IMoveValidator _moveValidator;
        private readonly ICheckVerifier _verifier;
        private readonly IBoard _board;
        private IBoard _previous;
        private bool _canUndo;

        public MoveManager() : this(new OrdinaryChessBoard())
        { }

        public MoveManager(IBoard board)
        {
            _board = board;
            _canUndo = false;
            _previous = board.Copy();
            _moveValidator = new OrdinaryBoardMoveValidator(_board);
            _verifier = new OrdinaryBoardCheckVerifier(_board, _moveValidator);
        }

        public (bool, string) CanMove(Position from, Position destination)
        {
            var figure = _board.FigureAt(from);
            if (figure == null)
            {
                return (false, "There is no figure at the given position");
            }
            if (!figure.CanMove(destination))
            {
                return (false, $"This move is not possible for a type figure: {figure.FigureType}");
            }

            var moveValidatorResult = _moveValidator.CanMove(figure, destination);
            if (!moveValidatorResult.Item1)
            {
                return moveValidatorResult;
            }

            if (_verifier.VerifyMoveCauseCheck(figure.Position, destination))
            {
                return (false, "You can't make this move, because it would put your king in a chequered position");
            }

            return (true, "OK");
        }

        public IMoveResult Move(Position @from, Position destination)
        {
            _canUndo = true;
            RewriteTheFigures(_board, _previous);
            
            var figure = _board.FigureAt(from);
            _board.RemoveFigure(from);
            var killed = _board.RemoveFigure(destination);
            figure.Move(destination);
            _board.SetFigure(figure, destination);
            var lastMoveViewModel = new LastMoveViewModel(figure, from, destination, killed);
            return new ValidMoveResult(_board, _verifier, _moveValidator, lastMoveViewModel);
        }

        public bool IsAllyAtPosition(Position position, TeamColor myTeamColor)
        {
            var figure = _board.FigureAt(position);
            return figure != null && figure.TeamColor == myTeamColor;
        }

        private void RewriteTheFigures(IBoard src, IBoard dst)
        {
            for (int i = 0; i < src.GetBoardSize(); i++)
            {
                for (int j = 0; j < src.GetBoardSize(); j++)
                {
                    if (src.FigureAt(src.GetPositionAt(i,j)) 
                        != dst.FigureAt(dst.GetPositionAt(i,j)))
                    {
                        Figure figure = src.FigureAt(src.GetPositionAt(i, j));
                        figure = figure == null ? null : figure.Copy();
                        dst.SetFigure(figure, dst.GetPositionAt(i,j));
                    }
                }
            }
        }
        
        public bool Undo()
        {
            if (!_canUndo)
            {
                return false;
            }
            _canUndo = false;
            RewriteTheFigures(_previous,_board);
            return true;
        }
    }
}