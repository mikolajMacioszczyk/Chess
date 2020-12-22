using Chess.Game.Team;
using Chess.Models.Figures;

namespace Chess.Game.MoveResult
{
    public interface IMoveResult
    {
        TeamColor Winner();
        (bool, Figure) IsCheck(TeamColor teamColor);
        bool IsCheckMate(TeamColor teamColor);
        int GetScore(TeamColor teamColor);
        (Figure, Models.Position.Position, Models.Position.Position) LastMoveFigureAndPositionFromAndDest();
        bool IsLastMoveSmash();
        Figure SmashedFigure();
    }
}