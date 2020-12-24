using Chess.Exceptions;
using Chess.Exceptions.InvalidBoardActionException;
using Chess.Game.MoveManager;
using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Position;
using Chess.ViewModels.Statictics;

namespace Chess.Game.GameManager
{
    public class GameManager : IGameManager
    {
        private IMoveManager _moveManager;
        private bool _isCheckMate;
        private TeamColor _currentMovingTeam;

        public GameManager() { }
        
        public void Start()
        {
            _moveManager = new MoveManager.MoveManager();
            _currentMovingTeam = TeamColor.White;
        }

        public IMoveResult DoMove(Position @from, Position destination)
        {
            if (!_moveManager.IsAllyAtPosition(from, _currentMovingTeam))
            {
                throw new InvalidTeamException(_currentMovingTeam, from);
            }

            if (_moveManager.CanMove(from, destination))
            {
                var result = _moveManager.Move(from, destination);
                SwitchTeam();
                _isCheckMate = result.IsCheckMate(_currentMovingTeam);
                return _moveManager.Move(from, destination);
            }

            throw new InvalidMoveException(from,destination);
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

        public GameStatisticsViewModel GetStatistics()
        {
            throw new System.NotImplementedException();
        }

        public bool Undo()
        {
            throw new System.NotImplementedException();
        }

        public bool Redo()
        {
            throw new System.NotImplementedException();
        }

        public TeamColor CurrentMoveTeam()
        {
            return _currentMovingTeam;
        }
    }
}