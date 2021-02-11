using Chess.Enums;
using Chess.Models.Board;
using Chess.ViewModels;
using NUnit.Framework;
using PlayerComputerAI.AI;

namespace Chess.UnitTests.PlayerComputerAIUnitTests
{
    [TestFixture]
    public class ComputerPLayerUnitTests
    {
        [Test]
        public void StartGame_White_Depth1_Breadth5()
        {
            // arrange
            var computer = new ComputerPlayer(TeamColor.White, 1, 5);
            var board = new OrdinaryChessBoard();
            var boardVm = new BoardViewModel(board);
            
            // act
            var move = computer.NextMove(boardVm);
            
            // assert
            Assert.NotNull(move);
            Assert.Greater(move.Score, 9);
                
            var figure = board.RemoveFigure(move.From);
            figure.Move(move.Destination);
            board.SetFigure(figure, move.Destination);
            
            Assert.AreEqual(move.Score, board.GetScoreForTeam(TeamColor.White));
        }
        
        [Test]
        public void StartGame_Black_Depth1_Breadth5()
        {
            // arrange
            var computer = new ComputerPlayer(TeamColor.Black, 1, 5);
            var board = new OrdinaryChessBoard();
            var boardVm = new BoardViewModel(board);
            
            // act
            var move = computer.NextMove(boardVm);
            
            // assert
            Assert.NotNull(move);
            Assert.Greater(move.Score, 9);

            var figure = board.RemoveFigure(move.From);
            figure.Move(move.Destination);
            board.SetFigure(figure, move.Destination);
            
            Assert.AreEqual(move.Score, board.GetScoreForTeam(TeamColor.Black));
        }

        [Test]
        public void StartGame_White_Depth2_Breadth5()
        {
            // arrange
            var computer = new ComputerPlayer(TeamColor.White, 2, 5);
            var board = new OrdinaryChessBoard();
            var boardVm = new BoardViewModel(board);

            // act
            var move = computer.NextMove(boardVm);

            // assert
            Assert.NotNull(move);
            Assert.AreEqual(0, move.Score);
        }
    }
}