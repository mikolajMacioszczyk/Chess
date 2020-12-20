using Chess.Figures;

namespace Chess.MoveValidator
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
        bool CanMove(Figure figure, Position.Position position);
    }
}