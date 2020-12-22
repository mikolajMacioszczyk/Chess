using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;

namespace Chess.Game.CheckVerfier
{
    public interface ICheckVerifier
    {
        void SetBoard(IBoard board);
        bool IsCheck(TeamColor teamColor);
        Figure FigureCausingCheck(TeamColor teamColor);
        bool VerifyMoveCauseCheck(Models.Position.Position from, Models.Position.Position destination);
    }
}