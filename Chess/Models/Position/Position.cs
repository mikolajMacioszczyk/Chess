using System;

namespace Chess.Models.Position
{
    [Serializable]
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static bool operator ==(Position left, Position right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return left.Row == right.Row && left.Column == right.Column;
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
            return HashCode.Combine(Row, Column);
        }

        protected bool Equals(Position other)
        {
            return Row == other.Row && Column == other.Column;
        }
        
        public override string ToString()
        {
            return $"[{Row+1} : {Column+1}]";
        }
    }
}