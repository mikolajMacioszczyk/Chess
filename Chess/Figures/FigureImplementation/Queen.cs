using Chess.GameManager;
using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Queen : Figure
    {
        public Queen(Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.Queen, teamColor)
        {
        }

        public override bool CanMove(Position.Position newPosition)
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