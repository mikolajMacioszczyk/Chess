using Chess.Figures;

namespace Chess.MoveValidator
{
    public interface IMoveValidator
    {
        bool CanMove(Figure board, Position.Position position);
    }
}