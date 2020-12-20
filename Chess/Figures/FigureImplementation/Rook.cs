using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Rook : Figure
    {
        public Rook(Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.Rook, teamColor)
        {
        }

        public override bool CanMove(Position.Position newPosition)
        {
            if (Position == newPosition)
            {
                return false;
            }
            var vector = new Vector(Position, newPosition);
            if (vector.IsHorizontal || vector.IsVertical)
            {
                return true;
            }
            return false;
        }

        public override Figure Copy()
        {
            return new Rook(Position, TeamColor);
        }
    }
}