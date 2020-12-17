using Chess.Figures;

namespace Chess.FigurePositionWeight
{
    public interface IFigurePositionWeight
    {
        public int GetPositionWeight(Figure figure);
        public int GetFigureTypeWeight(FigureType figureType);
    }
}