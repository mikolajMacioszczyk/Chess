using System;

namespace Chess.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public Position.Position OldPosition { get; set; }
        public Position.Position NewPosition { get; set; }
        public string Cause { get; set; }

        public InvalidPositionException(Position.Position oldPosition, Position.Position newPosition, string cause)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Cause = cause;
        }

        public override string Message => Cause;
    }
}