using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Figures;
using NUnit.Framework;

namespace Chess.UnitTests.ModelsUnitTests.BoardUnitTests
{
    [TestFixture]
    public class OrdinaryChessBoardUnitTests
    {
        [Test]
        public void DefaultConstructor_InitializationTest()
        {
            // arrange
                        
            // act
            var board = new OrdinaryChessBoard();
            var blackRock = board.FigureAt(new Models.Position.Position(0, 0));
            var whiteKnight = board.FigureAt(new Models.Position.Position(7, 1));
            var blackBishop = board.FigureAt(new Models.Position.Position(0, 5));
            var whiteQueen = board.FigureAt(new Models.Position.Position(7, 3));
            var blackKing = board.FigureAt(new Models.Position.Position(0, 4));
            var whitePawn = board.FigureAt(new Models.Position.Position(6, 2));

            // assert
            Assert.AreEqual(FigureType.Rook, blackRock.FigureType);
            Assert.AreEqual(TeamColor.Black, blackRock.TeamColor);
            Assert.AreEqual(FigureType.Knight, whiteKnight.FigureType);
            Assert.AreEqual(TeamColor.White, whiteKnight.TeamColor);
            Assert.AreEqual(FigureType.Bishop, blackBishop.FigureType);
            Assert.AreEqual(TeamColor.Black, blackBishop.TeamColor);
            Assert.AreEqual(FigureType.Queen, whiteQueen.FigureType);
            Assert.AreEqual(TeamColor.White, whiteQueen.TeamColor);
            Assert.AreEqual(FigureType.King, blackKing.FigureType);
            Assert.AreEqual(TeamColor.Black, blackKing.TeamColor);
            Assert.AreEqual(FigureType.Pawn, whitePawn.FigureType);
            Assert.AreEqual(TeamColor.White, whitePawn.TeamColor);
        }
        
        [Test]
        public void CopyConstructor_InitializationTest()
        {
            // arrange
            var baseBoard = new OrdinaryChessBoard();
            var expectedFigureType = FigureType.Knight;
            var destinationPosition = new Models.Position.Position(2, 1);
            var secondDestinationPosition = new Models.Position.Position(3, 0);
            
            // act
            var knight = baseBoard.RemoveFigure(new Models.Position.Position(0, 1));
            knight.Move(destinationPosition);
            baseBoard.SetFigure(knight, destinationPosition);
            var copyBoard = new OrdinaryChessBoard(baseBoard);
            baseBoard.RemoveFigure(destinationPosition);
            knight.Move(secondDestinationPosition);
            baseBoard.SetFigure(knight, secondDestinationPosition);
            
            // assert
            Assert.AreEqual(expectedFigureType, copyBoard.FigureAt(destinationPosition).FigureType);
            Assert.AreEqual(null, copyBoard.FigureAt(secondDestinationPosition));
        }

        [Test]
        public void SetRemoveOperation_Test()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Models.Position.Position(0, 1);
            var knightPosition1 = new Models.Position.Position(2, 1);
            var knightPosition2 = new Models.Position.Position(3, 0);

            // act
            var check0 = board.FigureAt(knightPosition1);
            var knight = board.RemoveFigure(startKnightPosition);
            board.SetFigure(knight, knightPosition1);
            var check1 = board.FigureAt(startKnightPosition);
            var check2 = board.FigureAt(knightPosition1);
            board.RemoveFigure(knightPosition1);
            board.SetFigure(knight, knightPosition2);
            var check3 = board.FigureAt(knightPosition1);
            var check4 = board.FigureAt(knightPosition2);
            // assert
            Assert.AreEqual(null, check0);
            Assert.AreEqual(null, check1);
            Assert.AreEqual(FigureType.Knight, check2.FigureType);
            Assert.AreEqual(null, check3);
            Assert.AreEqual(FigureType.Knight, check4.FigureType);
        }

        [Test]
        public void GetScoreForTeam_InitialConfiguration_ShouldBe0()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            TeamColor team = TeamColor.Black;
            int expectedScore = 0;
            
            // act
            var result = board.GetScoreForTeam(team);
            
            // assert
            Assert.AreEqual(expectedScore, result);
        }
        
        [Test]
        public void GetScoreForTeam_AfterMovementBlack_BottomLeftPawnTwoFieldUp_BlackTeam_ShouldBe_Minus1()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            TeamColor team = TeamColor.Black;
            int expectedScore = -1;
            var startPawnPosition = new Models.Position.Position(1, 0);
            var endPawnPosition = new Models.Position.Position(3, 0);
            
            // act
            var pawn = board.RemoveFigure(startPawnPosition);
            board.SetFigure(pawn, endPawnPosition);
            pawn.Move(endPawnPosition);
            var result = board.GetScoreForTeam(team);

            // assert
            Assert.AreEqual(expectedScore, result);
        }
        
        [Test]
        public void GetScoreForTeam_AfterMovementBlack_BottomLeftPawnTwoFieldUp_WhiteTeam_ShouldBe_1()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            TeamColor team = TeamColor.White;
            int expectedScore = 1;
            var startPawnPosition = new Models.Position.Position(1, 0);
            var endPawnPosition = new Models.Position.Position(3, 0);

            // act
            var pawn = board.RemoveFigure(startPawnPosition);
            pawn.Move(endPawnPosition);
            board.SetFigure(pawn, endPawnPosition);
            var result = board.GetScoreForTeam(team);

            // assert
            Assert.AreEqual(expectedScore, result);
        }
        
        [Test]
        public void GetScoreForTeam_AfterDeleteBlack_BottomLeftPawn_BlackTeam_ShouldBe_Minus30()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            TeamColor team = TeamColor.Black;
            int expectedScore = -21;
            var pawnPosition = new Models.Position.Position(1, 0);
            
            // act
            board.RemoveFigure(pawnPosition);
            var result = board.GetScoreForTeam(team);

            // assert
            Assert.AreEqual(expectedScore, result);
        }
        
        [Test]
        public void GetScoreForTeam_AfterDeleteBlack_BottomLeftPawn_WhiteTeam_ShouldBe_30()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            TeamColor team = TeamColor.White;
            int expectedScore = 21;
            var pawnPosition = new Models.Position.Position(1, 0);
            
            // act
            board.RemoveFigure(pawnPosition);
            var result = board.GetScoreForTeam(team);

            // assert
            Assert.AreEqual(expectedScore, result);
        }
    }
}