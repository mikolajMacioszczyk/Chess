using Chess.Enums;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Models.Board;
using Chess.Models.Position;
using Chess.ViewModels.LastMoveViewModel;

namespace Chess.Game.MoveManager
{
    public interface IMoveManager
    {
        (bool, string) CanMove(Position from, Position destination);
        (IBoard, ICheckVerifier, IMoveValidator, LastMoveViewModel) Move(Position from, Position destination);
        bool IsAllyAtPosition(Position position, TeamColor myTeamColor);
        bool Undo();
    }
}