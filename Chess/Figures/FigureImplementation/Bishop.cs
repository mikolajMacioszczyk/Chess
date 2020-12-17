using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Bishop : Figure
    {
        public Bishop(Position.Position position, TeamColor teamColor) : 
            base(position, FigureType.Bishop, teamColor)
        {
        }

        public override bool CanMove(Position.Position newPosition)
        {
            if (Position == newPosition)
            {
                return false;
            }
            var vector = new Vector(Position, newPosition);
            return vector.IsDiagonal;
        }
    }
}