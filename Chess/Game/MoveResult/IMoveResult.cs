using Chess.Game.Team;
using Chess.Models.Figures;
using Chess.ViewModels.BoardViewModel;

namespace Chess.Game.MoveResult
{
    public interface IMoveResult
    {
        bool IsCheck(TeamColor teamColor);
        Figure FigureCausingCheck(TeamColor teamColor);
        bool IsCheckMate(TeamColor teamColor);
        int GetScore(TeamColor teamColor);
        (Figure, Models.Position.Position, Models.Position.Position) LastMoveFigureAndPositionFromAndDest();
        bool IsLastMoveSmash();
        Figure SmashedFigure();
        BoardViewModel GetBoard();
    }
}