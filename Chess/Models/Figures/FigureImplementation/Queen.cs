using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.Models.Figures.FigureImplementation
{
    public class Queen : Figure
    {
        public Queen(Models.Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.Queen, teamColor)
        {
        }

        public override bool CanMove(Models.Position.Position newPosition)
        {
            if (Position == newPosition)
            {
                return false;
            }
            var vector = new Vector(Position, newPosition);
            if (vector.IsHorizontal || vector.IsVertical || vector.IsDiagonal)
            {
                return true;
            }
            return false;
        }

        public override Figure Copy()
        {
            return new Queen(Position, TeamColor);
        }
    }
}