using System;

namespace Chess.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public Models.Position.Position OldPosition { get; set; }
        public Models.Position.Position NewPosition { get; set; }
        public string Cause { get; set; }

        public InvalidPositionException(Models.Position.Position oldPosition, Models.Position.Position newPosition, string cause)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Cause = cause;
        }

        public override string Message => Cause;
    }
}