using Chess.Models.Position;

namespace Chess.ViewModels
{
    public struct MovePositions
    {
        public Position From { get; set; }
        public Position Destination { get; set; }
    }
}