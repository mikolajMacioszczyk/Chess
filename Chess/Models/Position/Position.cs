using System;

namespace Chess.Models.Position
{
    public class Position
    {
        public int PositionX { get; }
        public int PositionY { get; }

        public Position(int positionY, int positionX)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public static bool operator ==(Position left, Position right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return left.PositionX == right.PositionX && left.PositionY == right.PositionY;
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PositionX, PositionY);
        }

        protected bool Equals(Position other)
        {
            return PositionX == other.PositionX && PositionY == other.PositionY;
        }
        
        public override string ToString()
        {
            return $"[{PositionX} : {PositionY}]";
        }
    }
}