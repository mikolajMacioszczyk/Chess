using Chess.Enums;
using Chess.Game.MoveResult;
using Chess.Models.Position;

namespace Chess.Game.GameConductor
{
    public interface IGameConductor
    {
        IMoveResult Start();
        IMoveResult DoMove(Position from, Position destination);
        bool Undo();
        TeamColor CurrentMoveTeam();
    }
}