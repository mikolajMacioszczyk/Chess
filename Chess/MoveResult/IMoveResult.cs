using Chess.Figures;
using Chess.Team;

namespace Chess.MoveResult
{
    public interface IMoveResult
    {
        TeamColor Winner();
        (bool, Figure) IsCheck(TeamColor teamColor);
        bool IsCheckMate(TeamColor teamColor);
        int GetScore(TeamColor teamColor);
        (Figure, Position.Position, Position.Position) LastMoveFigureAndPositionFromAndDest();
        bool IsLastMoveSmash();
        Figure SmashedFigure();
    }
}