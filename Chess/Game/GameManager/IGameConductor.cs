using Chess.Enums;
using Chess.Game.MoveResult;
using Chess.Models.Position;
using Chess.ViewModels.Statictics;

namespace Chess.Game.GameManager
{
    public interface IGameConductor
    {
        IMoveResult Start();
        IMoveResult DoMove(Position from, Position destination);
        GameStatisticsViewModel GetStatistics();
        bool Undo();
        bool Redo();
        TeamColor CurrentMoveTeam();
    }
}