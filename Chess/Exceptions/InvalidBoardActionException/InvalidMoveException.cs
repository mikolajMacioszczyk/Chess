using Chess.Models.Figures;

namespace Chess.Exceptions.InvalidBoardActionException
{
    public class InvalidMoveException : InvalidBoardActionException
    {
        private Models.Position.Position _from;
        private Models.Position.Position _destination;
        private Figure _figure;

        public InvalidMoveException(Models.Position.Position @from, Models.Position.Position destination, Figure figure)
        {
            _from = @from;
            _destination = destination;
            _figure = figure;
        }
        
        public InvalidMoveException(Models.Position.Position @from, Models.Position.Position destination)
        {
            _from = @from;
            _destination = destination;
        }

        public override string Message
        {
            get
            {
                if (_figure == null)
                {
                    return $"Cannot move from {_from} to {_destination}";
                }
                return $"Cannot move figure {_figure} from {_from} to {_destination}";
            }
        }
    }
}