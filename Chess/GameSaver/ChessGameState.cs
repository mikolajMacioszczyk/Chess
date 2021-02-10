using System;
using System.Collections.Generic;
using Chess.Enums;
using Chess.Game.MoveResult;
using Chess.Models.Figures;
using Chess.Models.Player;

namespace Chess.GameSaver
{
    [Serializable]
    public class ChessGameState
    {
        public IMoveResult LastGameMoveResult { get; set; }
        public bool IsEnded { get; set; }
        public Player[] Players { get; set; }
        public TeamColor CurrentMovingTeam { get; set; }
        public PlayerMode PlayerMode { get; set; }
        public ChessGameState(
            IMoveResult lastGameMoveResult, 
            bool isEnded, 
            Player[] players, 
            TeamColor currentMovingTeam, 
            PlayerMode playerMode)
        {
            LastGameMoveResult = lastGameMoveResult;
            IsEnded = isEnded;
            Players = players;
            CurrentMovingTeam = currentMovingTeam;
            PlayerMode = playerMode;
        }
    }
}