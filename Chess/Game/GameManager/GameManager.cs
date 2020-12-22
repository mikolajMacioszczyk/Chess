using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;

namespace Chess.Game.GameManager
{
    public class GameManager : IGameManager
    {
        public static readonly int BoardSize = 8;
        private readonly IMoveValidator _moveValidator;
        private IBoard _board;

        public GameManager(IMoveValidator moveValidator)
        {
            _moveValidator = moveValidator;
            _board = new OrdinaryChessBoard();
        }

        public bool CanMove(Figure figure, Models.Position.Position position)
        {
            if (!_moveValidator.CanMove(figure, position))
            {
                return false;
            }
            throw new System.NotImplementedException();
        }

        public IMoveResult Move(Figure figure, Models.Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEnemyAtPosition(Models.Position.Position position, TeamColor myTeamColor)
        {
            throw new System.NotImplementedException();
        }

        public bool IsAllyAtPosition(Models.Position.Position position, TeamColor myTeamColor)
        {
            throw new System.NotImplementedException();
        }

        public bool IsPositionFree(Models.Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public int GetCurrentScore()
        {
            throw new System.NotImplementedException();
        }
    }
}