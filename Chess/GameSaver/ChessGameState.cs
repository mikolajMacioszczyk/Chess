using System;
using Chess.Enums;
using Chess.Game.MoveResult;
using Chess.Models.Board;

namespace Chess.GameSaver
{
    [Serializable]
    public struct ChessGameState
    {
        public IMoveResult LastGameMoveResult { get; set; }
        public bool IsEnded { get; set; }
        public TeamColor CurrentMovingTeam { get; set; }
        public PlayerMode PlayerMode { get; set; }
    }
}