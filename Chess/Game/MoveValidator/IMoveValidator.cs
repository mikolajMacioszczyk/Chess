using Chess.Models.Board;
using Chess.Models.Figures;

namespace Chess.Game.MoveValidator
{
    public interface IMoveValidator
    {
        /// <summary>
        /// Expects Move is Legal for this type of Figure
        /// Check if position is not out of board
        /// Check if other figure not block move
        /// Check if move not cause Check
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        (bool, string) CanMove(Figure figure, Models.Position.Position position);

        void Update(IBoard board);
    }
}