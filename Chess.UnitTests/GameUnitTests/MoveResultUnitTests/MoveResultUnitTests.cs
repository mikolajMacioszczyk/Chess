using NUnit.Framework;

namespace Chess.UnitTests.GameUnitTests.MoveResultUnitTests
{
    /// <summary>
    /// Winner
    /// IsCheck
    /// IsCheckMate
    /// </summary>
    [TestFixture]
    public class MoveResultUnitTests
    {
//         [Test]
//         public void GetScore_AfterMovementBlack_BottomLeftPawnTwoFieldUp_BlackTeam_ShouldBe_Minus1()
//         {
//             // arrange
//             var startingBoard = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.Black;
//             var startPawnPosition = new Position.Position(1, 0);
//             var endPawnPosition = new Position.Position(3, 0);
//             
//             var pawn = startingBoard.RemoveFigure(startPawnPosition);
//             startingBoard.SetFigure(pawn, endPawnPosition);
//             pawn.Move(endPawnPosition);
//             var moveResult =
//                 new MoveResult.MoveResult(startingBoard, pawn, startPawnPosition, endPawnPosition, null, null);
//             
//             int expectedScore = -1;
//             // act
//             var result = moveResult.GetScore(team);
//
//             // assert
//             Assert.AreEqual(expectedScore, result);
//         }
//         
//         [Test]
//         public void GetScore_AfterMovementBlack_BottomLeftPawnTwoFieldUp_WhiteTeam_ShouldBe_1()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startPawnPosition = new Position.Position(1, 0);
//             var endPawnPosition = new Position.Position(3, 0);
//
//             var pawn = board.RemoveFigure(startPawnPosition);
//             pawn.Move(endPawnPosition);
//             board.SetFigure(pawn, endPawnPosition);
//             var moveResult = new MoveResult.MoveResult(board, pawn,startPawnPosition,endPawnPosition,null, null);
//             
//             int expectedScore = 1;
//             // act
//             var result = moveResult.GetScore(team);
//
//             // assert
//             Assert.AreEqual(expectedScore, result);
//         }
//         
//         [Test]
//         public void GetScore_AfterKilledWhite_BlackTeam_ShouldBe_Plus19()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.Black;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             board.RemoveFigure(oneHopWhitePawnPosition);
//             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
//             blackPawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
//
//             var moveResult = new MoveResult.MoveResult(board, blackPawn, oneHopBlackPawnPosition,
//                 oneHopWhitePawnPosition, whitePawn, null);
//             
//             int expectedScore = 19;
//             
//             // act
//             var result = moveResult.GetScore(team);
//
//             // assert
//             Assert.AreEqual(expectedScore, result);
//         }
//         
//         [Test]
//         public void GetScore_AfterKilledWhite_WhiteTeam_ShouldBe_Minus19()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             board.RemoveFigure(oneHopWhitePawnPosition);
//             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
//             blackPawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
//             
//             var moveResult = new MoveResult.MoveResult(board, blackPawn, oneHopBlackPawnPosition,
//                 oneHopWhitePawnPosition, whitePawn, null);
//             
//             int expectedScore = -19;
//             
//             // act
//             var result = moveResult.GetScore(team);
//
//             // assert
//             Assert.AreEqual(expectedScore, result);
//         }
//
//         [Test]
//         public void IsLastMoveSmash_Smash_ShouldReturnTrue()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             board.RemoveFigure(oneHopWhitePawnPosition);
//             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
//             blackPawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
//             
//             var moveResult = new MoveResult.MoveResult(board, blackPawn, oneHopBlackPawnPosition,
//                 oneHopWhitePawnPosition, whitePawn, null);
//             
//             // act
//             var result = moveResult.IsLastMoveSmash(); 
//
//             // assert
//             Assert.True(result);
//         }
//         
//         [Test]
//         public void IsLastMoveSmash_NotSmash_ShouldReturnFalse()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             var moveResult = new MoveResult.MoveResult(board, whitePawn, startWhitePawnPosition,
//                 oneHopWhitePawnPosition, null, null);
//             
//             // act
//             var result = moveResult.IsLastMoveSmash(); 
//
//             // assert
//             Assert.False(result);
//         }
//         
//         [Test]
//         public void SmashedFigure_SmashedWhitePawn_ShouldThisFigure()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             board.RemoveFigure(oneHopWhitePawnPosition);
//             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
//             blackPawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
//             
//             var moveResult = new MoveResult.MoveResult(board, blackPawn, oneHopBlackPawnPosition,
//                 oneHopWhitePawnPosition, whitePawn, null);
//
//             var expected = whitePawn;
//             
//             // act
//             var isSmash = moveResult.IsLastMoveSmash();
//             var result = moveResult.SmashedFigure(); 
//
//             // assert
//             Assert.True(isSmash);
//             Assert.AreEqual(expected, result);
//         }
//
//         [Test]
//         public void LastMoveFigureAndPositionFromAndDest_Smash()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//
//             board.RemoveFigure(oneHopWhitePawnPosition);
//             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
//             blackPawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
//             
//             var moveResult = new MoveResult.MoveResult(board, blackPawn, oneHopBlackPawnPosition,
//                 oneHopWhitePawnPosition, whitePawn, null);
//
//             var expectedLastMoveFigure = blackPawn;
//             var expectedLastMovePositionFrom = oneHopBlackPawnPosition;
//             var expectedLastMovePositionDest = oneHopWhitePawnPosition;
//             
//             // act
//             var result = moveResult.LastMoveFigureAndPositionFromAndDest(); 
//
//             // assert
//             Assert.AreEqual(expectedLastMoveFigure, result.Item1);
//             Assert.AreEqual(expectedLastMovePositionFrom, result.Item2);
//             Assert.AreEqual(expectedLastMovePositionDest, result.Item3);
//         }
//         
//         [Test]
//         public void LastMoveFigureAndPositionFromAndDest_NotSmash()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             TeamColor team = TeamColor.White;
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var oneHopBlackPawnPosition = new Position.Position(3, 5);
//             var startWhitePawnPosition = new Position.Position(6, 4);
//             var oneHopWhitePawnPosition = new Position.Position(4, 4);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(oneHopBlackPawnPosition);
//             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
//             
//             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
//             whitePawn.Move(oneHopWhitePawnPosition);
//             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
//             
//             var moveResult = new MoveResult.MoveResult(board, whitePawn, startWhitePawnPosition,
//                 oneHopWhitePawnPosition, null, null);
//
//             var expectedLastMoveFigure = whitePawn;
//             var expectedLastMovePositionFrom = startWhitePawnPosition;
//             var expectedLastMovePositionDest = oneHopWhitePawnPosition;
//             
//             // act
//             var result = moveResult.LastMoveFigureAndPositionFromAndDest(); 
//
//             // assert
//             Assert.AreEqual(expectedLastMoveFigure, result.Item1);
//             Assert.AreEqual(expectedLastMovePositionFrom, result.Item2);
//             Assert.AreEqual(expectedLastMovePositionDest, result.Item3);
//         }
//
//         [Test]
//         public void IsCheck_BlackTeam_NotCheck_ShouldReturnFalse()
//         {
//             // arrange
//             var board = new OrdinaryChessBoard();
//             var startBlackPawnPosition = new Position.Position(1, 5);
//             var endBlackPawnPosition = new Position.Position(3, 5);
//
//             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
//             blackPawn.Move(endBlackPawnPosition);
//             board.SetFigure(blackPawn, endBlackPawnPosition);
//             var moveResult = new MoveResult.MoveResult(
//                 board, blackPawn, startBlackPawnPosition,
//                 endBlackPawnPosition, null, null);
//
//             TeamColor teamColor = TeamColor.Black;
//             
//             // act
//             var result = moveResult.IsCheck(teamColor);
//             
//             // assert
//             Assert.False(result.Item1);
//         }
//         
//         [Test]
//         public void IsCheck_BlackTeam_Check_Bishop_ShouldReturnTrue()
//         {
//             
//         }
//         
//         [Test]
//         public void IsCheck_WhiteTeam_Check_Bishop_ShouldReturnTrue()
//         {
//             
//         }
//         
//         [Test]
//         public void IsCheck_BlackTeam_Check_Knight_ShouldReturnTrue()
//         {
//             
//         }
//         
//         [Test]
//         public void IsCheck_WhiteTeam_Check_Knight_ShouldReturnTrue()
//         {
//             
//         }
//         
//         [Test]
//         public void IsCheck_BlackTeam_OtherTeamChecked_ShouldReturnFalse()
//         {
//             
//         }
//         
//         [Test]
//         public void IsCheck_WhiteTeam_OtherTeamChecked_ShouldReturnFalse()
//         {
//             
//         }
//         
//         // jedni tak a sprawdzam drugich
    }
}