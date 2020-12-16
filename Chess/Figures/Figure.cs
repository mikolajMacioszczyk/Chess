using Chess.Exceptions;
using Chess.GameManager;
using Chess.Team;

namespace Chess.Figures
{
    public abstract class Figure
    {
        protected Position.Position Position;
        private FigureType _figureType;
        protected TeamColor _teamColor;
        protected readonly IGameManager GameManager;

        protected Figure(Position.Position position, FigureType figureType, TeamColor teamColor, IGameManager gameManager)
        {
            Position = position;
            _figureType = figureType;
            _teamColor = teamColor;
            GameManager = gameManager;
        }

        public virtual Position.Position Move(Position.Position newPosition)
        {
            if (!CanMove(newPosition))
            {
                throw new InvalidPositionException(Position, newPosition, $"Invalid move by figure {this}");
            }
            if (!GameManager.CanMove(this, newPosition))
            {
                throw new InvalidPositionException(Position, newPosition, $"Move not allowe by game manger");
            }
            var oldPosition = Position;
            Position = newPosition;
            return oldPosition;
        }
        public abstract bool CanMove(Position.Position newPosition);
    }
}