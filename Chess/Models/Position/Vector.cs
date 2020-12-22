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
        public IEnumerable<Models.Position.Position> GetPath()
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
            return new List<Models.Position.Position>();
        }
        private IEnumerable<Models.Position.Position> FindHorizontalPath()
        {
            List<Models.Position.Position> output = new List<Models.Position.Position>();
            if (DiffX > 0)
            {
                for (int i = Start.PositionX + 1; i < End.PositionX; i++)
                {
                    output.Add(new Models.Position.Position(Start.PositionY, i));
                }
            }
            else
            {
                for (int i = Start.PositionX - 1; i > End.PositionX; i--)
                {
                    output.Add(new Models.Position.Position(Start.PositionY, i));
                }
            }
            return output;
        }
        private IEnumerable<Models.Position.Position> FindVerticalPath()
        {
            List<Models.Position.Position> output = new List<Models.Position.Position>();
            if (DiffY > 0)
            {
                for (int i = Start.PositionY + 1; i < End.PositionY; i++)
                {
                    output.Add(new Models.Position.Position(i, Start.PositionX));
                }
            }
            else
            {
                for (int i = Start.PositionY - 1; i > End.PositionY; i--)
                {
                    output.Add(new Models.Position.Position(i, Start.PositionX));
                }
            }
            return output;
        }
        private IEnumerable<Models.Position.Position> FindDiagonalPath()
        {
            List<Models.Position.Position> output = new List<Models.Position.Position>();
            if (DiffX > 0)
            {
                for (int i = 1; i < DiffX; i++)
                {
                    output.Add(new Models.Position.Position(Start.PositionY+i, Start.PositionX+i));
                }
            }
            else
            {
                for (int i = -DiffX-1; i > 0; i--)
                {
                    output.Add(new Models.Position.Position(End.PositionY+i, End.PositionX+i));
                }
            }
            return output;
        }
    }
}