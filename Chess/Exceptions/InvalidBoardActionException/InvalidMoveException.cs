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

        public override string Message => $"Cannot move figure {_figure} from {_from} to {_destination}";
    }
}