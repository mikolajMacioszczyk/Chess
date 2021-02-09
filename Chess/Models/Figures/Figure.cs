using System;
using Chess.Game.Team;

namespace Chess.Models.Figures
{
    [Serializable]
    public abstract class Figure
    {
        public Models.Position.Position Position { get; private set;}
        public FigureType FigureType { get; }
        public TeamColor TeamColor { get; }

        protected Figure(Models.Position.Position position, FigureType figureType, TeamColor teamColor)
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
        public virtual Models.Position.Position Move(Models.Position.Position newPosition)
        {
            var oldPosition = Position;
            Position = newPosition;
            return oldPosition;
        }
        public abstract bool CanMove(Models.Position.Position newPosition);
        public abstract Figure Copy();
    }
}