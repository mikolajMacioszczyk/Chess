using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.Models.Figures.FigureImplementation
{
    public class Rook : Figure
    {
        public Rook(Models.Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.Rook, teamColor)
        {
        }

        public override bool CanMove(Models.Position.Position newPosition)
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