using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.Models.Figures.FigureImplementation
{
    public class Bishop : Figure
    {
        public Bishop(Models.Position.Position position, TeamColor teamColor) : 
            base(position, FigureType.Bishop, teamColor)
        {
        }

        public override bool CanMove(Models.Position.Position newPosition)
        {
            if (Position == newPosition)
            {
                return false;
            }
            var vector = new Vector(Position, newPosition);
            return vector.IsDiagonal;
        }

        public override Figure Copy()
        {
            return new Bishop(Position, TeamColor);
        }
    }
}