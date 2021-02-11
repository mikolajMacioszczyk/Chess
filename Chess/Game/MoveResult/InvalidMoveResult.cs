using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Models.Figures;
using Chess.Models.Position;
using Chess.ViewModels;

namespace Chess.Game.MoveResult
{
    public class InvalidMoveResult : IMoveResult
    {
        private readonly string CallbackInfo = "Last move was not valid. Firstly check why that happened.";
        private readonly string _cause;
        public InvalidMoveResult(string cause)
        {
            _cause = cause;
        }
        
        public IsValidMoveResult IsValidMove()
        {
            return new IsValidMoveResult(MoveResultStatus.Invalid, _cause);
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