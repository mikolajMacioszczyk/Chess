using System;
using System.Linq;
using Chess.Exceptions;
using Chess.FigurePositionWeight;
using Chess.Figures;
using Chess.Figures.FigureImplementation;
using Chess.Team;

namespace Chess.Board
{
    public class OrdinaryChessBoard : IBoard
    {
        public static readonly int BoardSize = 8;
        private IFigurePositionWeight _weights = new OrdinaryChessBoardFigurePositionWeight();
        private Figure[][] _board;

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

        public Figure FigureAt(Position.Position position)
        {
            CheckPosition(position);
            return _board[position.PositionY][position.PositionX];
        }

        public void SetFigure(Figure figure, Position.Position position)
        {
            CheckPosition(position);
            _board[position.PositionY][position.PositionX] = figure;
        }

        public Figure RemoveFigure(Position.Position position)
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

        public Figure GetFigureAtPosition(Position.Position position)
        {
            CheckPosition(position);
            return _board[position.PositionY][position.PositionX];
        }

        private void CheckPosition(Position.Position position)
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
            _board[0][0] = new Rook(new Position.Position(0, 0), TeamColor.Black);
            _board[0][BoardSize-1] = new Rook(new Position.Position(0, BoardSize-1), TeamColor.Black);
            
            _board[BoardSize-1][0] = new Rook(new Position.Position(BoardSize-1, 0), TeamColor.White);
            _board[BoardSize-1][BoardSize-1] = new Rook(new Position.Position(BoardSize-1, BoardSize-1), TeamColor.White);
        }

        private void InitializeBishops()
        {
            _board[0][2] = new Bishop(new Position.Position(0, 2), TeamColor.Black);
            _board[0][BoardSize-3] = new Bishop(new Position.Position(0, BoardSize-3), TeamColor.Black);
            
            _board[BoardSize-1][2] = new Bishop(new Position.Position(BoardSize-1, 2), TeamColor.White);
            _board[BoardSize-1][BoardSize-3] = new Bishop(new Position.Position(BoardSize-1, BoardSize-3), TeamColor.White);
        }

        private void InitializeKingFamilies()
        {
            _board[0][3] = new Queen(new Position.Position(0, 3), TeamColor.Black);
            _board[0][4] = new King(new Position.Position(0, 4), TeamColor.Black);
            
            _board[BoardSize-1][4] = new Queen(new Position.Position(BoardSize-1, 4), TeamColor.White);
            _board[BoardSize-1][3] = new King(new Position.Position(BoardSize-1, 3), TeamColor.White);
        }
        
        private void InitializeKnights()
        {
            _board[0][1] = new Knight(new Position.Position(0, 1), TeamColor.Black);
            _board[0][BoardSize-2] = new Knight(new Position.Position(0, BoardSize-2), TeamColor.Black);
            _board[BoardSize-1][BoardSize-2] = new Knight(new Position.Position(BoardSize-1, BoardSize-2), TeamColor.White);
            _board[BoardSize-1][1] = new Knight(new Position.Position(BoardSize-1, 1), TeamColor.White);
        }

        private void InitializePawns()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                _board[1][i] = new Pawn(new Position.Position(1, i), TeamColor.Black);
                _board[BoardSize-2][i] = new Pawn(new Position.Position(BoardSize-2, i), TeamColor.White);
            }
        }
    }
}