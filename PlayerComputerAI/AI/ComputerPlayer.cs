using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Enums;
using Chess.Game.MoveManager;
using Chess.Models.Board;
using Chess.Models.Figures;
using Chess.ViewModels;
using PlayerComputerAI.Exceptions;

namespace PlayerComputerAI.AI
{
    public class ComputerPlayer
    {
        private readonly int _searchDepth;
        private readonly int _breadthSearch;
        public TeamColor MyTeamColor { get; }
        private readonly TeamColor _opponentTeamColor;

        public ComputerPlayer(TeamColor teamColor, int searchDepth, int breadthSearch)
        {
            MyTeamColor = teamColor; 
            _opponentTeamColor = teamColor == TeamColor.White ? TeamColor.Black : TeamColor.White;
            _searchDepth = Math.Max(searchDepth, 1);
            _breadthSearch = Math.Max(breadthSearch, 1);
        }

        public ScoreAndMove NextMove(BoardViewModel boardVm)
        {
            var board = boardVm.GetCopy();
            var moveManager = new MoveManager(board);
            var possibleMoves = GetAllPossibleMoves(MyTeamColor, moveManager, board);
            var score = GetScore(board, possibleMoves, _searchDepth, false);
            if (score.HasValue)
            {
                return score.Value;
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

            List<(int, MovePositions, IBoard)> sortedScores = TransformAndSortScoresDependingOnTeam(isOpponentMove, boardsAfterMoves);

            if (!sortedScores.Any())
            {
                return null;
            }
            if (currentDepth <= 1)
            {
                return new ScoreAndMove(sortedScores[0].Item1, sortedScores[0].Item2.From, sortedScores[0].Item2.Destination);
            }
            
            // recursive call
            return GetResultFromDeeperSearch(sortedScores, currentDepth, isOpponentMove);
        }

        private List<(int, MovePositions, IBoard)> TransformAndSortScoresDependingOnTeam(
            bool isOpponentMove, IEnumerable<(IBoard, MovePositions)> data)
        {
            var scores = data
                .Select(bm => (bm.Item1.GetScoreForTeam(MyTeamColor), bm.Item2, bm.Item1));
            if (isOpponentMove)
                return scores.OrderBy(s => s.Item1).ToList();
            return scores.OrderByDescending(s => s.Item1).ToList();
        }

        private ScoreAndMove? GetResultFromDeeperSearch(List<(int, MovePositions, IBoard)> scores, int currentDepth, bool isOpponentMove)
        {
            scores = scores.Take(_breadthSearch).ToList();
            var maxScore = new ScoreAndMove(){Score = isOpponentMove ? Int32.MaxValue : Int32.MinValue};
            foreach (var score in scores)
            {
                var board = score.Item3;
                var moveManager = new MoveManager(board);
                var moves = 
                    GetAllPossibleMoves(isOpponentMove ? MyTeamColor : _opponentTeamColor, moveManager, board);
                var deeperResult = GetScore(board, moves, currentDepth - 1, !isOpponentMove);
                if (deeperResult.HasValue)
                {
                    if (!isOpponentMove && deeperResult.Value.Score > maxScore.Score
                    || isOpponentMove && deeperResult.Value.Score < maxScore.Score)
                    {
                        maxScore.Score = deeperResult.Value.Score;
                        maxScore.From = score.Item2.From;
                        maxScore.Destination = score.Item2.Destination;
                    }
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
                        output.AddRange(GetPossibleMovesForFigure(figure, moveManager, board));
                    }
                }
            }
            return output;
        }

        private List<MovePositions> GetPossibleMovesForFigure(Figure figure, IMoveManager moveManager, IBoard board)
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