using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.Game.MoveManager
{
    public interface IMoveManager
    {
        bool CanMove(Position from, Position destination);
        IMoveResult Move(Position from, Position destination);
        bool IsEnemyAtPosition(Position position, TeamColor myTeamColor);
        bool IsAllyAtPosition(Position position, TeamColor myTeamColor);
        bool IsPositionFree(Position position);
    }
}