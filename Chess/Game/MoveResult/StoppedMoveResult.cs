using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Models.Figures;
using Chess.Models.Position;
using Chess.ViewModels;

namespace Chess.Game.MoveResult
{
    public class StoppedMoveResult : IMoveResult
    {
        private static string CallbackInfo = "Game has been stopped.";
        public IsValidMoveResult IsValidMove()
        {
            return new IsValidMoveResult(MoveResultStatus.Stopped, "");
        }

        public bool IsCheck(TeamColor teamColor)
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public Figure FigureCausingCheck(TeamColor teamColor)
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public bool IsCheckMate(TeamColor teamColor)
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public int GetScore(TeamColor teamColor)
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public (Figure, Position, Position) LastMoveFigureAndPositionFromAndDest()
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public bool IsLastMoveSmash()
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public Figure SmashedFigure()
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public IEnumerable<Figure> AllSmashedFigures()
        {
            throw new InvalidOperationException(CallbackInfo);
        }

        public BoardViewModel GetBoard()
        {
            throw new InvalidOperationException(CallbackInfo);
        }
    }
}