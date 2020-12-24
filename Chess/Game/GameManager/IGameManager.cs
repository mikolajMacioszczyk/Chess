using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Position;
using Chess.ViewModels.Statictics;

namespace Chess.Game.GameManager
{
    public interface IGameManager
    {
        void Start();
        IMoveResult DoMove(Position from, Position destination);
        GameStatisticsViewModel GetStatistics();
        bool Undo();
        bool Redo();
        TeamColor CurrentMoveTeam();
    }
}