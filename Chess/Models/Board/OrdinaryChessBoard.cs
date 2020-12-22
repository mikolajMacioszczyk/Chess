using System.Linq;
using Chess.Exceptions;
using Chess.Game.Team;
using Chess.Models.Board.FigurePositionWeight;
using Chess.Models.Figures;
using Chess.Models.Figures.FigureImplementation;

namespace Chess.Models.Board
{
    public class OrdinaryChessBoard : IBoard
    {
        public static readonly int BoardSize = 8;
        private IFigurePositionWeight _weights = new OrdinaryChessBoardFigurePositionWeight();
        private Figure[][] _board;
        
        private static Models.Position.Position[][] _positionsXY = new []
        {
            new []{new Models.Position.Position(0,0), new Models.Position.Position(1,0), new Models.Position.Position(2,0), new Models.Position.Position(3,0), new Models.Position.Position(4,0), new Models.Position.Position(5,0), new Models.Position.Position(6,0), new Models.Position.Position(7,0)},
            new []{new Models.Position.Position(0,1), new Models.Position.Position(1,1), new Models.Position.Position(2,1), new Models.Position.Position(3,1), new Models.Position.Position(4,1), new Models.Position.Position(5,1), new Models.Position.Position(6,1), new Models.Position.Position(7,1)},
            new []{new Models.Position.Position(0,2), new Models.Position.Position(1,2), new Models.Position.Position(2,2), new Models.Position.Position(3,2), new Models.Position.Position(4,2), new Models.Position.Position(5,2), new Models.Position.Position(6,2), new Models.Position.Position(7,2)},
            new []{new Models.Position.Position(0,3), new Models.Position.Position(1,3), new Models.Position.Position(2,3), new Models.Position.Position(3,3), new Models.Position.Position(4,3), new Models.Position.Position(5,3), new Models.Position.Position(6,3), new Models.Position.Position(7,3)},
            new []{new Models.Position.Position(0,4), new Models.Position.Position(1,4), new Models.Position.Position(2,4), new Models.Position.Position(3,4), new Models.Position.Position(4,4), new Models.Position.Position(5,4), new Models.Position.Position(6,4), new Models.Position.Position(7,4)},
            new []{new Models.Position.Position(0,5), new Models.Position.Position(1,5), new Models.Position.Position(2,5), new Models.Position.Position(3,5), new Models.Position.Position(4,5), new Models.Position.Position(5,5), new Models.Position.Position(6,5), new Models.Position.Position(7,5)},
            new []{new Models.Position.Position(0,6), new Models.Position.Position(1,6), new Models.Position.Position(2,6), new Models.Position.Position(3,6), new Models.Position.Position(4,6), new Models.Position.Position(5,6), new Models.Position.Position(6,6), new Models.Position.Position(7,6)},
            new []{new Models.Position.Position(0,7), new Models.Position.Position(1,7), new Models.Position.Position(2,7), new Models.Position.Position(3,7), new Models.Position.Position(4,7), new Models.Position.Position(5,7), new Models.Position.Position(6,7), new Models.Position.Position(7,7)},
        };

        public OrdinaryChessBoard(OrdinaryChessBoard other)
        {
            _board = new Figure[BoardSize][];
            for (int i = 0; i < BoardSize; i++)
            {
                _board[i] = other._board[i].ToArray();
            }
            
        }

        /// <summary>
        /// Default constructor initialize game
        /// </summary>
        public OrdinaryChessBoard()
        {
            _board = new Figure[BoardSize][];
            for (int i = 0; i < BoardSize; i++)
            {
                _board[i] = new Figure[BoardSize];
            }
            InitializeFigures();
        }

        public int GetBoardSize()
        {
            return BoardSize;
        }

        public Figure FigureAt(Models.Position.Position position)
        {
            CheckPosition(position);
            return _board[position.PositionY][position.PositionX];
        }

        public void SetFigure(Figure figure, Models.Position.Position position)
        {
            CheckPosition(position);
            _board[position.PositionY][position.PositionX] = figure;
        }

        public Figure RemoveFigure(Models.Position.Position position)
        {
            CheckPosition(position);
            Figure removed = _board[position.PositionY][position.PositionX];
            _board[position.PositionY][position.PositionX] = null;
            return removed;
        }

        public int GetScoreForTeam(TeamColor myTeamColor)
        {
            int sum = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (_board[i][j] != null)
                    {
                        Figure figure = _board[i][j];
                        if (figure.TeamColor == myTeamColor)
                        {
                            sum += _weights.GetPositionWeight(figure);
                            sum += _weights.GetFigureTypeWeight(figure.FigureType);
                        }
                        else
                        {
                            sum -= _weights.GetPositionWeight(figure);
                            sum -= _weights.GetFigureTypeWeight(figure.FigureType);
                        }
                    }
                }
            }
            return sum;
        }

        public Figure GetFigureAtPosition(Models.Position.Position position)
        {
            CheckPosition(position);
            return _board[position.PositionY][position.PositionX];
        }

        public Models.Position.Position GetPositionAt(int x, int y)
        {
            return _positionsXY[x][y];
        }

        public IBoard Copy()
        {
            return new OrdinaryChessBoard(this);
        }
        
        public King GetKing(TeamColor kingTeamColor)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var figure = _board[i][j];
                    if (figure != null && figure.FigureType == FigureType.King && figure.TeamColor == kingTeamColor)
                    {
                        return (King)figure;
                    }
                }
            }
            throw new ImplementationException("King not found");
        }

        private void CheckPosition(Models.Position.Position position)
        {
            if (position.PositionX < 0 || position.PositionX >= BoardSize
                                       || position.PositionY < 0 || position.PositionY >= BoardSize)
            {
                throw new InvalidPositionException(position, position, "Position out of board");
            }
        }
        
         private void InitializeFigures()
        {
            InitializeBishops();
            InitializeKnights();
            InitializePawns();
            InitializeRooks();
            InitializeKingFamilies();
        }

        private void InitializeRooks()
        {
            _board[0][0] = new Rook(new Models.Position.Position(0, 0), TeamColor.Black);
            _board[0][BoardSize-1] = new Rook(new Models.Position.Position(0, BoardSize-1), TeamColor.Black);
            
            _board[BoardSize-1][0] = new Rook(new Models.Position.Position(BoardSize-1, 0), TeamColor.White);
            _board[BoardSize-1][BoardSize-1] = new Rook(new Models.Position.Position(BoardSize-1, BoardSize-1), TeamColor.White);
        }

        private void InitializeBishops()
        {
            _board[0][2] = new Bishop(new Models.Position.Position(0, 2), TeamColor.Black);
            _board[0][BoardSize-3] = new Bishop(new Models.Position.Position(0, BoardSize-3), TeamColor.Black);
            
            _board[BoardSize-1][2] = new Bishop(new Models.Position.Position(BoardSize-1, 2), TeamColor.White);
            _board[BoardSize-1][BoardSize-3] = new Bishop(new Models.Position.Position(BoardSize-1, BoardSize-3), TeamColor.White);
        }

        private void InitializeKingFamilies()
        {
            _board[0][3] = new Queen(new Models.Position.Position(0, 3), TeamColor.Black);
            _board[0][4] = new King(new Models.Position.Position(0, 4), TeamColor.Black);
            
            _board[BoardSize-1][3] = new Queen(new Models.Position.Position(BoardSize-1, 3), TeamColor.White);
            _board[BoardSize-1][4] = new King(new Models.Position.Position(BoardSize-1, 4), TeamColor.White);
        }
        
        private void InitializeKnights()
        {
            _board[0][1] = new Knight(new Models.Position.Position(0, 1), TeamColor.Black);
            _board[0][BoardSize-2] = new Knight(new Models.Position.Position(0, BoardSize-2), TeamColor.Black);
            _board[BoardSize-1][BoardSize-2] = new Knight(new Models.Position.Position(BoardSize-1, BoardSize-2), TeamColor.White);
            _board[BoardSize-1][1] = new Knight(new Models.Position.Position(BoardSize-1, 1), TeamColor.White);
        }

        private void InitializePawns()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                _board[1][i] = new Pawn(new Models.Position.Position(1, i), TeamColor.Black);
                _board[BoardSize-2][i] = new Pawn(new Models.Position.Position(BoardSize-2, i), TeamColor.White);
            }
        }
    }
}