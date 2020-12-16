using System;

namespace Chess.Position
{
    public class Vector
    {
        public Position Start { get; }
        public Position End { get; }

        public int DiffX { get; }

        public int DiffY { get; }
        
        public Vector(Position start, Position end)
        {
            End = end;
            Start = start;
            DiffX = start.PositionX - end.PositionX;
            DiffY = start.PositionY - end.PositionY;
        }

        public double Length => Math.Sqrt(DiffX * DiffX + DiffY * DiffY);
        public bool IsDiagonal => Math.Abs(DiffX) - Math.Abs(DiffY) == 0;
        public bool IsVertical => DiffX == 0;
        public bool IsHorizontal => DiffY == 0;
    }
}