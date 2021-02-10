using Chess.Enums;
using Chess.Game.MoveManager;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Position;

namespace Chess.Game.GameManager
{
    public class GameConductor : IGameConductor
    {
        private IMoveManager _moveManager;
        private bool _isCheckMate;
        private TeamColor _currentMovingTeam;

        public GameConductor(ChessGameState state)
        {
            _currentMovingTeam = state.CurrentMovingTeam;
            _isCheckMate = state.IsEnded;
            _moveManager = new MoveManager.MoveManager(state.LastGameMoveResult.GetBoard().GetCopy());
        }

        public GameConductor()
        {
            _moveManager = new MoveManager.MoveManager();
            _currentMovingTeam = TeamColor.White;
        }

        public IMoveResult Start()
        {
            return new MoveResultStart();
        }

        public IMoveResult DoMove(Position @from, Position destination)
        {
            if (!_moveManager.IsAllyAtPosition(from, _currentMovingTeam))
            {
                return new InvalidMoveResult($"Cannot move opponent's figure");
            }

            if (_moveManager.CanMove(from, destination))
            {
                var result = _moveManager.Move(from, destination);
                SwitchTeam();
                _isCheckMate = result.IsCheckMate(_currentMovingTeam);
                return result;
            }

            return new InvalidMoveResult($"Cannot move from position {from} to position {destination}");
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