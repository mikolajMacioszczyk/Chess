using System;
using Chess.Enums;

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

        public static bool operator ==(Figure lhs, Figure rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            { return true; }
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            { return false; }
            return lhs.FigureType == rhs.FigureType && lhs.TeamColor == rhs.TeamColor;
        }

        public static bool operator !=(Figure lhs, Figure rhs)
        {
            return !(lhs == rhs);
        }
    }
}