using System.Collections.Generic;
using System.Linq;
using Chess.Enums;
using Chess.Game.MoveManager;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Figures;
using Chess.Models.Position;
using Chess.ViewModels;

namespace Chess.Game.GameConductor
{
    public class GameConductor : IGameConductor
    {
        private IMoveManager _moveManager;
        private bool _isCheckMate;
        private TeamColor _currentMovingTeam;
        private List<Figure> _smashed;
        private Figure _lastSmashed;

        public GameConductor(ChessGameState state)
        {
            _currentMovingTeam = state.CurrentMovingTeam;
            _isCheckMate = state.IsEnded;
            _smashed = state.LastGameMoveResult.AllSmashedFigures().ToList();
            _moveManager = new MoveManager.MoveManager(state.LastGameMoveResult.GetBoard().GetCopy());
        }


        public GameConductor()
        {
            _moveManager = new MoveManager.MoveManager();
            _currentMovingTeam = TeamColor.White;
            _smashed = new List<Figure>();
        }

        public IMoveResult Start()
        {
            return new MoveResultStart();
        }

        public IMoveResult DoMove(Position @from, Position destination)
        {
            if (!_moveManager.IsAllyAtPosition(from, _currentMovingTeam))
            {
                return new InvalidMoveResult($"Your figure is not in the given position");
            }

            var moveManagerValidation = _moveManager.CanMove(from, destination);
            if (!moveManagerValidation.Item1)
            {
                return new InvalidMoveResult(moveManagerValidation.Item2);
            }
            
            var result = _moveManager.Move(from, destination);
            AddNewSmashed(result.Item4);
            var moveResult = new ValidMoveResult(result.Item1, result.Item2, result.Item3, result.Item4, _smashed);
            SwitchTeam();
            _isCheckMate = moveResult.IsCheckMate(_currentMovingTeam);
            return moveResult;
        }

        private void AddNewSmashed(LastMoveViewModel lastMoveViewModel)
        {
            _lastSmashed = null;
            if (lastMoveViewModel.Smashed != null)
            {
                _smashed.Add(lastMoveViewModel.Smashed);
                _lastSmashed = lastMoveViewModel.Smashed;
            }
        }

        private void SwitchTeam()
        {
            if (_currentMovingTeam == TeamColor.Black)
            {
                _currentMovingTeam = TeamColor.White;
            }
            else
            {
                _currentMovingTeam = TeamColor.Black;
            }
        }

        public bool Undo()
        {
            if (_moveManager.Undo())
            {
                SwitchTeam();
                if (_lastSmashed != null)
                {
                    _smashed.Remove(_lastSmashed);
                }
                return true;
            }
            return false;
        }

        public TeamColor CurrentMoveTeam()
        {
            return _currentMovingTeam;
        }
    }
}