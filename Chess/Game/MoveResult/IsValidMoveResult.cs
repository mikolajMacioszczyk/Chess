namespace Chess.Game.MoveResult
{
    public readonly struct IsValidMoveResult
    {
        public readonly bool IsValid { get; }
        public readonly string Cause { get; }

        public IsValidMoveResult(bool isValid, string cause)
        {
            IsValid = isValid;
            Cause = cause;
        }
    }
}