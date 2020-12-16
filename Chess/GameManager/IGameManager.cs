using Chess.Figures;
using Chess.Team;

namespace Chess.GameManager
{
    public interface IGameManager
    {
        bool CanMove(Figure figure, Position.Position position);
        bool IsEnemyAtPosition(Position.Position position, TeamColor myTeamColor);
    }
}