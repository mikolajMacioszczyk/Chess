using Chess.GameManager;
using Chess.Position;
using Chess.Team;

namespace Chess.Figures.FigureImplementation
{
    public class King : Figure
    {
        public King(Position.Position position, TeamColor teamColor, IGameManager gameManager) 
            : base(position, FigureType.King, teamColor, gameManager)
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
    }
}