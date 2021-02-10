using Chess.Enums;
using Chess.Game.MoveResult;
using Chess.Models.Position;

namespace Chess.Game.MoveManager
{
    public interface IMoveManager
    {
        bool CanMove(Position from, Position destination);
        IMoveResult Move(Position from, Position destination);
        bool IsAllyAtPosition(Position position, TeamColor myTeamColor);
        bool Undo();
    }
}