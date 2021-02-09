using System;
using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Board;

namespace Chess.GameSaver
{
    [Serializable]
    public struct ChessGameState
    {
        public IMoveResult LastGameMoveResult { get; set; }
        public bool IsEnded { get; set; }
        public TeamColor CurrentMovingTeam { get; set; }
    }
}