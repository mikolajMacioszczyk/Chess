using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Enums;
using Chess.Game.MoveManager;
using Chess.Game.MoveResult;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.ViewModels;
using PlayerComputerAI.Exceptions;

namespace PlayerComputerAI.AI
{
    public class ComputerPlayer
    {
        private readonly int _searchDepth;
        private int _breadthSearch;
        public string Name { get; }
        private readonly TeamColor _myTeamColor;
        private TeamColor _opponentTeamColor;

        public ComputerPlayer(TeamColor teamColor, int searchDepth, int breadthSearch)
        {
            _myTeamColor = teamColor; 
            _opponentTeamColor = teamColor == TeamColor.White ? TeamColor.Black : TeamColor.White;
            Name = "Computer";
            _searchDepth = Math.Max(searchDepth, 1);
            _breadthSearch = Math.Max(breadthSearch, 1);
        }

        public MovePositions NextMove(IMoveResult lastMoveResult)
        {
            var board = lastMoveResult.GetBoard().GetCopy();
            var moveManager = new MoveManager(board);
            var possibleMoves = GetAllPossibleMoves(_myTeamColor, moveManager, board);
            var score = GetScore(board, possibleMoves, _searchDepth, false);
            if (score.HasValue)
            {
                return new MovePositions() {From = score.Value.From, Destination = score.Value.Destination};
            }
            throw new ImpossibleMoveException();
        }
    
        /// <summary>
        /// Returns <value>_breadthSearch</value> bests scores 
        /// </summary>
        /// <param name="originalBoard">Original board</param>
        /// <param name="possibleMoves">All possible moves</param>
        /// <param name="currentDepth"></param>
        /// <param name="isOpponentMove"></param>
        /// <returns></returns>
        private ScoreAndMove? GetScore(IBoard originalBoard, IEnumerable<MovePositions> possibleMoves, int currentDepth, bool isOpponentMove)
        {
            var boardsAfterMoves = possibleMoves.Select(movePosition =>
            {
                var board = originalBoard.Copy();
                var figure = board.RemoveFigure(movePosition.From);
                figure.Move(movePosition.Destination);
                board.SetFigure(figure, movePosition.Destination);
                return (board, movePosition);
            });
            
            List<(int, MovePositions, IBoard)> scores = boardsAfterMoves
                .Select(bm => (bm.board.GetScoreForTeam(_myTeamColor), bm.movePosition, bm.board)).ToList();

            scores = (isOpponentMove ? scores.OrderBy(s => s.Item1) : scores.OrderByDescending(s => s.Item1))
                .ToList();

            if (currentDepth == 0)
            {
                if (scores.Any())
                {
                    return new ScoreAndMove(){Score = scores[0].Item1, From = scores[0].Item2.From, Destination = scores[1].Item2.Destination};
                }
                return null;
            }
            
            // recursive call
            if (!scores.Any())
            {
                return null;
            }
            scores = scores.Take(_breadthSearch).ToList();
            var maxScore = new ScoreAndMove() {Score = scores[0].Item1, From = scores[0].Item2.From, Destination = scores[0].Item2.Destination};
            foreach (var score in scores)
            {
                var board = score.Item3;
                var moveManager = new MoveManager(board);
                var moves = 
                    GetAllPossibleMoves(isOpponentMove ? _myTeamColor : _opponentTeamColor, moveManager, board);
                var deeperResult = GetScore(board, moves, currentDepth - 1, !isOpponentMove);
                if (deeperResult.HasValue && deeperResult.Value.Score > maxScore.Score)
                {
                    maxScore.Score = deeperResult.Value.Score;
                    maxScore.From = deeperResult.Value.From;
                    maxScore.Destination = deeperResult.Value.Destination;
                }
            }
            return maxScore;
        }

        private List<MovePositions> GetAllPossibleMoves(TeamColor teamColor, IMoveManager moveManager, IBoard board)
        {
            var output = new List<MovePositions>();
            for (int i = 0; i < board.GetBoardSize(); i++)
            {
                for (int j = 0; j < board.GetBoardSize(); j++)
                {
                    var figure = board.FigureAt(board.GetPositionAt(i, j));
                    if (figure != null  && figure.TeamColor == teamColor)
                    {
                        output.AddRange(GetPossibleMoves(figure, moveManager, board));
                    }
                }
            }
            return output;
        }

        private List<MovePositions> GetPossibleMoves(Figure figure, IMoveManager moveManager, IBoard board)
        {
            var output = new List<MovePositions>();
            for (int i = 0; i < board.GetBoardSize(); i++)
            {
                for (int j = 0; j < board.GetBoardSize(); j++)
                {
                    if (moveManager.CanMove(figure.Position, board.GetPositionAt(i,j)).Item1)
                    {
                        output.Add(new MovePositions(){From = figure.Position, Destination = board.GetPositionAt(i, j)});
                    }
                }
            }
            return output;
        }
    }
}