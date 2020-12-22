using Chess.Game.Team;
using Chess.Models.Figures;
using Chess.Models.Figures.FigureImplementation;

namespace Chess.Models.Board
{
    public interface IBoard
    {
        public int GetBoardSize();

        /// <summary>
        /// If field not empty, return Figure
        /// else returns null
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Figure FigureAt(Models.Position.Position position);
        /// <summary>
        /// Does not check if position is valid,
        /// Other class must provide that functionality
        /// </summary>
        public void SetFigure(Figure figure, Models.Position.Position position);
        public Figure RemoveFigure(Models.Position.Position position);
        public int GetScoreForTeam(TeamColor teamColor);
        public Figure GetFigureAtPosition(Models.Position.Position position);
        
        /// <summary>
        /// Return instance of position at indicated coordinates
        /// if out of bounds - throw OutOfBoundsException
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Models.Position.Position GetPositionAt(int x, int y);

        IBoard Copy();
        King GetKing(TeamColor kingTeamColor);
    }
}