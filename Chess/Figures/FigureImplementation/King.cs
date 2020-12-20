using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class King : Figure
    {
        public King(Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.King, teamColor)
        {
        }

        public override bool CanMove(Position.Position newPosition)
        {
            if (newPosition == Position)
            {
                return false;
            }
            if (new Vector(Position, newPosition).Length < 2)
            {
                return true;
            }
            return false;
        }

        public override Figure Copy()
        {
            return new King(Position, TeamColor);
        }
    }
}