using Chess.Figures;
using Chess.MoveResult;
using Chess.MoveValidator;
using Chess.Team;

namespace Chess.GameManager
{
    public class GameManager : IGameManager
    {
        public static readonly int BoardSize = 8;
        private readonly IMoveValidator _moveValidator;
        
        public bool CanMove(Figure figure, Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public IMoveResult Move(Figure figure, Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEnemyAtPosition(Position.Position position, TeamColor myTeamColor)
        {
            throw new System.NotImplementedException();
        }

        public bool IsAllyAtPosition(Position.Position position, TeamColor myTeamColor)
        {
            throw new System.NotImplementedException();
        }

        public bool IsPositionFree(Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public int GetCurrentScore()
        {
            throw new System.NotImplementedException();
        }
    }
}