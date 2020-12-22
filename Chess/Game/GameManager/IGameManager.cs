using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Figures;

namespace Chess.Game.GameManager
{
    public interface IGameManager
    {
        bool CanMove(Figure figure, Models.Position.Position position);
        IMoveResult Move(Figure figure, Models.Position.Position position);
        bool IsEnemyAtPosition(Models.Position.Position position, TeamColor myTeamColor);
        bool IsAllyAtPosition(Models.Position.Position position, TeamColor myTeamColor);
        bool IsPositionFree(Models.Position.Position position);
        int GetCurrentScore();
    }
}