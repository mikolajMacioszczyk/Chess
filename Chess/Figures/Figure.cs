using Chess.Team;

namespace Chess.Figures
{
    public abstract class Figure
    {
        public Position.Position Position { get; private set;}
        public FigureType FigureType { get; }
        public TeamColor TeamColor { get; }

        protected Figure(Position.Position position, FigureType figureType, TeamColor teamColor)
        {
            Position = position;
            FigureType = figureType;
            TeamColor = teamColor;
        }

        /// <summary>
        /// Does not check if move is valid
        /// </summary>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        public virtual Position.Position Move(Position.Position newPosition)
        {
            var oldPosition = Position;
            Position = newPosition;
            return oldPosition;
        }
        public abstract bool CanMove(Position.Position newPosition);
    }
}