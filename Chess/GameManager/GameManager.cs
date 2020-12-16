using Chess.Figures;
using Chess.Team;

namespace Chess.GameManager
{
    public class GameManager : IGameManager
    {
        public static readonly int BoardSize = 8;
        public bool CanMove(Figure figure, Position.Position position)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEnemyAtPosition(Position.Position position, TeamColor myTeamColor)
        {
            throw new System.NotImplementedException();
        }
    }
}