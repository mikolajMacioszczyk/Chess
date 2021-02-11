﻿using Chess.Models.Position;

namespace PlayerComputerAI
{
    public struct ScoreAndMove
    {
        public int Score { get; set; }
        public Position From { get; set; }
        public Position Destination { get; set; }
    }
}