using System;
using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Knight : Figure
    {
        public Knight(Position.Position position, TeamColor teamColor)
            : base(position, FigureType.Knight, teamColor)
        {
        }

        public override bool CanMove(Position.Position newPosition)
        {
            if (Position == newPosition)
            {
                return false;
            }

            var vector = new Vector(Position, newPosition);
            var diffX = Math.Abs(vector.DiffX);
            var diffY = Math.Abs(vector.DiffY);
            if (diffX == 2 && diffY == 1 ||
                diffX == 1 && diffY == 2)
            {
                return true;
            }
            return false;
        }

        public override Figure Copy()
        {
            return new Knight(Position, TeamColor);
        }
    }
}