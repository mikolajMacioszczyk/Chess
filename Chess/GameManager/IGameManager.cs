using Chess.Figures;
using Chess.MoveResult;
using Chess.Team;

namespace Chess.GameManager
{
    public interface IGameManager
    {
        bool CanMove(Figure figure, Position.Position position);
        IMoveResult Move(Figure figure, Position.Position position);
        bool IsEnemyAtPosition(Position.Position position, TeamColor myTeamColor);
        bool IsAllyAtPosition(Position.Position position, TeamColor myTeamColor);
        bool IsPositionFree(Position.Position position);
        int GetCurrentScore();
    }
}