using Chess.GameManager;
using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class Rook : Figure
    {
        public Rook(Position.Position position, TeamColor teamColor, IGameManager gameManager) 
            : base(position, FigureType.Rook, teamColor, gameManager)
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
    }
}