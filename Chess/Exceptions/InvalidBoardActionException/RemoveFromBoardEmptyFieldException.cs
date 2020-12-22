namespace Chess.Exceptions.InvalidBoardActionException
{
    public class RemoveFromBoardEmptyFieldException : InvalidBoardActionException
    {
        private Models.Position.Position _removePosition;

        public RemoveFromBoardEmptyFieldException(Models.Position.Position removePosition)
        {
            _removePosition = removePosition;
        }

        public override string Message => $"Try to remove from position {_removePosition}, but field was empty.";
    }
}