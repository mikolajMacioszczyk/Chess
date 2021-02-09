using System;

namespace Chess.Game.MoveResult
{
    [Serializable]
    public readonly struct IsValidMoveResult
    {
        public MoveResultStatus Status { get; }
        public string Cause { get; }

        public IsValidMoveResult(MoveResultStatus status, string cause)
        {
            Status = status;
            Cause = cause;
        }
    }
}