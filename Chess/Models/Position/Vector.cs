using System;
using System.Collections.Generic;

namespace Chess.Models.Position
{
    public class Vector
    {
        public Models.Position.Position Start { get; }
        public Models.Position.Position End { get; }

        public int DiffX { get; }

        public int DiffY { get; }
        
        public Vector(Models.Position.Position start, Models.Position.Position end)
        {
            End = end;
            Start = start;
            DiffX = end.Row - start.Row;
            DiffY = end.Column - start.Column;
        }

        public double Length => Math.Sqrt(DiffX * DiffX + DiffY * DiffY);
        public bool IsDiagonal => Math.Abs(DiffX) - Math.Abs(DiffY) == 0;
        public bool IsVertical => DiffY == 0;
        public bool IsHorizontal => DiffX == 0;

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
            if (DiffY > 0)
            {
                for (int i = Start.Column + 1; i < End.Column; i++)
                {
                    output.Add(new Position(Start.Row, i));
                }
            }
            else
            {
                for (int i = Start.Column - 1; i > End.Column; i--)
                {
                    output.Add(new Position(Start.Row, i));
                }
            }
            return output;
        }
        private IEnumerable<Position> FindVerticalPath()
        {
            List<Position> output = new List<Position>();
            if (DiffX > 0)
            {
                for (int i = Start.Row + 1; i < End.Row; i++)
                {
                    output.Add(new Position(i, Start.Column));
                }
            }
            else
            {
                for (int i = Start.Row - 1; i > End.Row; i--)
                {
                    output.Add(new Position(i, Start.Column));
                }
            }
            return output;
        }
        private IEnumerable<Position> FindDiagonalPath()
        {
            List<Position> output = new List<Position>();
            if (DiffY > 0)
            {
                if (DiffX > 0)
                {
                    for (int i = 1; i < DiffY; i++)
                    {
                        output.Add(new Position(Start.Row+i, Start.Column+i));
                    }
                }
                else
                {
                    for (int i = 1; i < DiffY; i++)
                    {
                        output.Add(new Position(Start.Row-i, Start.Column+i));
                    }
                }
            }
            else
            {
                if (DiffX < 0)
                {
                    for (int i = -DiffY-1; i > 0; i--)
                    {
                        output.Add(new Position(End.Row+i, End.Column+i));
                    }
                }
                else
                {
                    for (int i = -DiffY-1; i > 0; i--)
                    {
                        output.Add(new Position(End.Row-i, End.Column+i));
                    }
                }
            }
            return output;
        }
    }
}