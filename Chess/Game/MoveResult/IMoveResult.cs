using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Models.Figures;
using Chess.ViewModels;

namespace Chess.Game.MoveResult
{
    public interface IMoveResult
    {
        IsValidMoveResult IsValidMove();
        bool IsCheck(TeamColor teamColor);
        Figure FigureCausingCheck(TeamColor teamColor);
        bool IsCheckMate(TeamColor teamColor);
        int GetScore(TeamColor teamColor);
        (Figure, Models.Position.Position, Models.Position.Position) LastMoveFigureAndPositionFromAndDest();
        bool IsLastMoveSmash();
        Figure SmashedFigure();
        IEnumerable<Figure> AllSmashedFigures();
        BoardViewModel GetBoard();
    }
}