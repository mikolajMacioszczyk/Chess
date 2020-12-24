using Chess.Models.Figures;
using Chess.Models.Position;

namespace Chess.ViewModels.LastMoveViewModel
{
    public class LastMoveViewModel
    {
        public Figure FigureMoved { get;}
        public Position From { get; }
        public Position Destination { get; }
        public Figure Smashed { get; }
        
        public LastMoveViewModel(Figure figureMoved, Position @from, Position destination, Figure smashed)
        {
            FigureMoved = figureMoved;
            From = @from;
            Destination = destination;
            Smashed = smashed;
        }
    }
}