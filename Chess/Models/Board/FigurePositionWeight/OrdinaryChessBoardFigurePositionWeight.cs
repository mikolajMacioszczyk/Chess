using System;
using Chess.Game.Team;
using Chess.Models.Figures;

namespace Chess.Models.Board.FigurePositionWeight
{
    public class OrdinaryChessBoardFigurePositionWeight : IFigurePositionWeight
    {
        private static readonly int[][] QueenWeights =
        {
            new [] { -4,-2,-2,-1,-1,-2,-2,-4},
            new [] { -2,0,1,0,0,0,0,-2},
            new [] { -2,1,1,1,1,1,0,-2},
            new [] { 0,0,1,1,1,1,0,-1},
            new [] { -1,0,1,1,1,1,0,-1},
            new [] { -2,0,1,1,1,1,0,-2},
            new [] { -2,0,0,0,0,0,0,-2},
            new [] { -4,-2,-2,-1,-1,-2,-2,-4},
        };
        
        private static readonly int[][] KingWeights =
        {
            new[] {4, 6, 2, 0, 0, 2, 6, 4},
            new[] {4, 4, 0, 0, 0, 0, 4, 4},
            new[] {-2, -4, -4, -4, -4, -4, -4, -2},
            new[] {-4, -6, -6, -8, -8, -6, -6, -4},
            new[] {-6, -8, -8, -10, -10, -8, -8, -6},
            new[] {-6, -8, -8, -10, -10, -8, -8, -6},
            new[] {-6, -8, -8, -10, -10, -8, -8, -6},
            new[] {-6, -8, -8, -10, -10, -8, -8, -6},
        };

        private static readonly int[][] RockWeights =
        {
            new[] {0,0,0,1,1,0,0,0},
            new[] {-1,0,0,0,0,0,0,-1},
            new[] {-1,0,0,0,0,0,0,-1},
            new[] {-1,0,0,0,0,0,0,-1},
            new[] {-1,0,0,0,0,0,0,-1},
            new[] {-1,0,0,0,0,0,0,-1},
            new[] {1,2,2,2,2,2,2,1},
            new[] {0,0,0,0,0,0,0,0},
        };

        private static readonly int[][] BishopWeights =
        {
            new[] {-4, -2, -2, -2, -2, -2, -2, -4},
            new[] {-2, 1, 0, 0, 0, 0, 1, -2},
            new[] {-2, 2, 2, 2, 2, 2, 2, -2},
            new[] {-2, 0, 2, 2, 2, 2, 0, -2},
            new[] {-2, 1, 1, 2, 2, 1, 1, -2},
            new[] {-2, 0, 1, 2, 2, 1, 0, -2},
            new[] {-2, 0,0,0,0,0,0,-2},
            new[] {-4,-2,-2,-2,-2,-2,-2,-4},
        };

        private static readonly int[][] KnightWeights =
        {
            new[] {-10, -8, -6, -6, -6, -6, -8, -10},
            new[] {-8, -4, 0, 1, 1, 0, -2, -8},
            new[] {-6, 1, 2, 3, 3, 2, 1, -6},
            new[] {-6, 0, 3, 4, 4, 3, 0, -6},
            new[] {-6,1,3,4,4,3,1,-6},
            new[] {-6,0,2,3,3,2,0,-6},
            new[] {-8,-4,0,0,0,0,-4,-8},
            new[] {-10,-8,-6,-6,-6,-6,-8,-10},
        };

        private static readonly int[][] PawnWeights =
        {
            new[] {0, 0, 0, 0, 0, 0, 0, 0},
            new[] {1, 2, 2, -4, -4, 2, 2, 1},
            new[] {1, -1, -2, 0, 0, -2, -1, 1},
            new[] {0, 0, 0, 2, 2, 0, 0, 0},
            new[] {1, 1, 2, 3, 5, 5, 2, 1, 1},
            new[] {2,2,4,6,6,4,2,2},
            new[] {10,10,10,10,10,10,10,10},
            new[] {0,0,0,0,0,0,0,0},
        };
        
        public int GetPositionWeight(Figure figure)
        {
            switch (figure.TeamColor)
            {
                case TeamColor.White:
                    return GetWhiteTeamWeight(figure.FigureType, figure.Position);
                case TeamColor.Black:
                    return GetBlackTeamWeight(figure.FigureType, figure.Position);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int GetFigureTypeWeight(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.King:
                    return 1800;
                case FigureType.Queen:
                    return 180;
                case FigureType.Rook:
                    return 100;
                case FigureType.Bishop:
                    return 60;
                case FigureType.Knight:
                    return 60;
                case FigureType.Pawn:
                    return 20;
                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }

        private int[][] GetTableWeights(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.King:
                    return KingWeights;
                case FigureType.Queen:
                    return QueenWeights;
                case FigureType.Rook:
                    return RockWeights;
                case FigureType.Bishop:
                    return BishopWeights;
                case FigureType.Knight:
                    return KnightWeights;
                case FigureType.Pawn:
                    return PawnWeights;
                default:
                    throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }

        private int GetWhiteTeamWeight(FigureType figureType, Models.Position.Position position)
        {
            int[][] weights = GetTableWeights(figureType);
            return weights[position.Row][position.Column];
        }
        
        private int GetBlackTeamWeight(FigureType figureType, Models.Position.Position position)
        {
            int[][] weights = GetTableWeights(figureType);
            return weights[OrdinaryChessBoard.BoardSize - 1 - position.Row][OrdinaryChessBoard.BoardSize - 1 - position.Column];
        }
    }
}