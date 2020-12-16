using System;
using Chess.GameManager;
using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Bishop : Figure
    {
        public Bishop(Position.Position position, TeamColor teamColor, IGameManager gameManager)
            : base(position, FigureType.Bishop, teamColor, gameManager)
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
    }
}