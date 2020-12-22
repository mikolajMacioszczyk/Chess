using Chess.Models.Figures;

namespace Chess.Models.Board.FigurePositionWeight
{
    public interface IFigurePositionWeight
    {
        public int GetPositionWeight(Figure figure);
        public int GetFigureTypeWeight(FigureType figureType);
    }
}