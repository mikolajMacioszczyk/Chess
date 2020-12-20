using System;
using System.Collections.Generic;

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
            DiffX = end.PositionX - start.PositionX;
            DiffY = end.PositionY - start.PositionY;
        }

        public double Length => Math.Sqrt(DiffX * DiffX + DiffY * DiffY);
        public bool IsDiagonal => Math.Abs(DiffX) - Math.Abs(DiffY) == 0;
        public bool IsVertical => DiffX == 0;
        public bool IsHorizontal => DiffY == 0;

        /// <summary>
        /// If Vector is Horizontal, Vertical or Diagonal
        /// Returns appropriate path
        /// else returns Empty Collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> GetPath()
        {
            if (IsHorizontal)
            {
                return FindHorizontalPath();
            }
            if (IsVertical)
            {
                return FindVerticalPath();
            }
            if (IsDiagonal)
            {
                return FindDiagonalPath();
            }
            return new List<Position>();
        }
        private IEnumerable<Position> FindHorizontalPath()
        {
            List<Position> output = new List<Position>();
            if (DiffX > 0)
            {
                for (int i = Start.PositionX + 1; i < End.PositionX; i++)
                {
                    output.Add(new Position(Start.PositionY, i));
                }
            }
            else
            {
                for (int i = Start.PositionX - 1; i > End.PositionX; i--)
                {
                    output.Add(new Position(Start.PositionY, i));
                }
            }
            return output;
        }
        private IEnumerable<Position> FindVerticalPath()
        {
            List<Position> output = new List<Position>();
            if (DiffY > 0)
            {
                for (int i = Start.PositionY + 1; i < End.PositionY; i++)
                {
                    output.Add(new Position(i, Start.PositionX));
                }
            }
            else
            {
                for (int i = Start.PositionY - 1; i > End.PositionY; i--)
                {
                    output.Add(new Position(i, Start.PositionX));
                }
            }
            return output;
        }
        private IEnumerable<Position> FindDiagonalPath()
        {
            List<Position> output = new List<Position>();
            if (DiffX > 0)
            {
                for (int i = 1; i < DiffX; i++)
                {
                    output.Add(new Position(Start.PositionY+i, Start.PositionX+i));
                }
            }
            else
            {
                for (int i = -DiffX-1; i > 0; i--)
                {
                    output.Add(new Position(End.PositionY+i, End.PositionX+i));
                }
            }
            return output;
        }
    }
}