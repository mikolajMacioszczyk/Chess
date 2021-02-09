using System;
using Chess.Enums;
using Chess.Models.Position;

namespace Chess.Models.Figures.FigureImplementation
{
    [Serializable]
    public class King : Figure
    {
        public King(Models.Position.Position position, TeamColor teamColor) 
            : base(position, FigureType.King, teamColor)
        {
        }

        public override bool CanMove(Models.Position.Position newPosition)
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