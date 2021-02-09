using System;
using Chess.Enums;
using Chess.Models.Position;

namespace Chess.Models.Figures.FigureImplementation
{
    [Serializable]
    public class Pawn : Figure
    {
        private bool _isFirstMove;
        
        public Pawn(Models.Position.Position position, TeamColor teamColor)
            : base(position, FigureType.Pawn, teamColor)
        {
            _isFirstMove = true;
        }

        public override Models.Position.Position Move(Models.Position.Position newPosition)
        {
            var oldPos = base.Move(newPosition);
            _isFirstMove = false;
            return oldPos;
        }

        public override bool CanMove(Models.Position.Position newPosition)
        {
            if (!IsForward(newPosition))
            {
                return false;
            }

            var vector = new Vector(Position, newPosition);

            return IsLegalVerticalMove(vector) || IsLegalDiagonalMove(vector);
        }

        public override Figure Copy()
        {
            return new Pawn(Position, TeamColor);
        }

        /// <summary>
        /// White Team may only go to lower values of Y
        /// Black Team opposite
        /// </summary>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        private bool IsForward(Models.Position.Position newPosition)
        {
            if (TeamColor == TeamColor.Black)
            {
                return newPosition.Row < Position.Row;
            }
            return newPosition.Row > Position.Row;
        }

        /// <summary>
        /// Pawn may go vertical about 2 position, if it is first movement
        /// Otherwise about 1 position
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private bool IsLegalVerticalMove(Vector vector)
        {
            if (vector.IsVertical)
            {
                if (_isFirstMove)
                {
                    return vector.Length < 2.000001;
                }

                return Math.Abs(vector.Length - 1) < 0.000001;
            }
            return false;
        }
        
        /// <summary>
        /// Pawn may attack enemy on diagonal if he is 1 place away in X and Y dimension
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private bool IsLegalDiagonalMove(Vector vector)
        {
            return vector.IsDiagonal && Math.Abs(vector.DiffX) == 1 && Math.Abs(vector.DiffY) == 1;
        }
    }
}