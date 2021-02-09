using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Position;
using Chess.ViewModels.LastMoveViewModel;
using NUnit.Framework;

namespace Chess.UnitTests.GameUnitTests.MoveResultUnitTests
{
    [TestFixture]
    public class MoveResultUnitTests
    {
         [Test]
         public void GetScore_AfterMovementBlack_BottomLeftPawnTwoFieldUp_WhiteTeam_ShouldBe_Minus1()
         {
             // arrange
             var startingBoard = new OrdinaryChessBoard();
             TeamColor team = TeamColor.White;
             var startPawnPosition = new Position(1, 0);
             var endPawnPosition = new Position(3, 0);
             
             var pawn = startingBoard.RemoveFigure(startPawnPosition);
             startingBoard.SetFigure(pawn, endPawnPosition);
             pawn.Move(endPawnPosition);
             var validator = new OrdinaryBoardMoveValidator(startingBoard);
             var verifier = new OrdinaryBoardCheckVerifier(startingBoard, validator);
             var lastMoveVm = new LastMoveViewModel(pawn, startPawnPosition, endPawnPosition, null);
             var moveResult =
                 new ValidMoveResult(startingBoard, verifier,null, lastMoveVm);
             
             int expectedScore = -1;
             // act
             var result = moveResult.GetScore(team);

             // assert
             Assert.AreEqual(expectedScore, result);
         }
         
         [Test]
         public void GetScore_AfterMovementBlack_BottomLeftPawnTwoFieldUp_BlackTeam_ShouldBe_1()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             TeamColor team = TeamColor.Black;
             var startPawnPosition = new Position(1, 0);
             var endPawnPosition = new Position(3, 0);

             var pawn = board.RemoveFigure(startPawnPosition);
             pawn.Move(endPawnPosition);
             board.SetFigure(pawn, endPawnPosition);
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(pawn, startPawnPosition, endPawnPosition, null);
             var moveResult = new ValidMoveResult(board, verifier,null, lastMoveVm);
             
             int expectedScore = 1;
             // act
             var result = moveResult.GetScore(team);

             // assert
             Assert.AreEqual(expectedScore, result);
         }
         
         [Test]
         public void GetScore_AfterKilledWhite_WhiteTeam_ShouldBe_Plus19()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             TeamColor team = TeamColor.White;
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             board.RemoveFigure(oneHopWhitePawnPosition);
             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
             blackPawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(blackPawn, oneHopWhitePawnPosition);

             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm =
                 new LastMoveViewModel(blackPawn, oneHopBlackPawnPosition, oneHopWhitePawnPosition, whitePawn);
             var moveResult = new ValidMoveResult(board, verifier, null, lastMoveVm);
             
             int expectedScore = 19;
             
             // act
             var result = moveResult.GetScore(team);

             // assert
             Assert.AreEqual(expectedScore, result);
         }
         
         [Test]
         public void GetScore_AfterKilledWhite_BlackTeam_ShouldBe_Minus19()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             TeamColor team = TeamColor.Black;
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             board.RemoveFigure(oneHopWhitePawnPosition);
             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
             blackPawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);

             var lastMoveVm =
                 new LastMoveViewModel(blackPawn, oneHopBlackPawnPosition, oneHopWhitePawnPosition, whitePawn);
             var moveResult = new ValidMoveResult(board, verifier, null, lastMoveVm);
             
             int expectedScore = -19;
             
             // act
             var result = moveResult.GetScore(team);

             // assert
             Assert.AreEqual(expectedScore, result);
         }

         [Test]
         public void IsLastMoveSmash_Smash_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             board.RemoveFigure(oneHopWhitePawnPosition);
             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
             blackPawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm =
                 new LastMoveViewModel(blackPawn, oneHopBlackPawnPosition, oneHopWhitePawnPosition, whitePawn);
             var moveResult = new ValidMoveResult(board, verifier,null, lastMoveVm);
             
             // act
             var result = moveResult.IsLastMoveSmash(); 

             // assert
             Assert.True(result);
         }
         
         [Test]
         public void IsLastMoveSmash_NotSmash_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(whitePawn, startWhitePawnPosition, oneHopWhitePawnPosition, null);
             var moveResult = new ValidMoveResult(board, verifier,null, lastMoveVm);
             
             // act
             var result = moveResult.IsLastMoveSmash(); 

             // assert
             Assert.False(result);
         }
         
         [Test]
         public void SmashedFigure_SmashedWhitePawn_ShouldThisFigure()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             board.RemoveFigure(oneHopWhitePawnPosition);
             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
             blackPawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm =
                 new LastMoveViewModel(blackPawn, oneHopBlackPawnPosition, oneHopWhitePawnPosition, whitePawn);
             var moveResult = new ValidMoveResult(board, verifier, null, lastMoveVm);

             var expected = whitePawn;
             
             // act
             var isSmash = moveResult.IsLastMoveSmash();
             var result = moveResult.SmashedFigure(); 

             // assert
             Assert.True(isSmash);
             Assert.AreEqual(expected, result);
         }

         [Test]
         public void LastMoveFigureAndPositionFromAndDest_Smash()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);

             board.RemoveFigure(oneHopWhitePawnPosition);
             blackPawn = board.RemoveFigure(oneHopBlackPawnPosition);
             blackPawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(blackPawn, oneHopWhitePawnPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm =
                 new LastMoveViewModel(blackPawn, oneHopBlackPawnPosition, oneHopWhitePawnPosition, whitePawn);
             var moveResult = new ValidMoveResult(board, verifier, null, lastMoveVm);

             var expectedLastMoveFigure = blackPawn;
             var expectedLastMovePositionFrom = oneHopBlackPawnPosition;
             var expectedLastMovePositionDest = oneHopWhitePawnPosition;
             
             // act
             var result = moveResult.LastMoveFigureAndPositionFromAndDest(); 

             // assert
             Assert.AreEqual(expectedLastMoveFigure, result.Item1);
             Assert.AreEqual(expectedLastMovePositionFrom, result.Item2);
             Assert.AreEqual(expectedLastMovePositionDest, result.Item3);
         }
         
         [Test]
         public void LastMoveFigureAndPositionFromAndDest_NotSmash()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var oneHopBlackPawnPosition = new Position(3, 5);
             var startWhitePawnPosition = new Position(6, 4);
             var oneHopWhitePawnPosition = new Position(4, 4);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(oneHopBlackPawnPosition);
             board.SetFigure(blackPawn, oneHopBlackPawnPosition);
             
             var whitePawn = board.RemoveFigure(startWhitePawnPosition);
             whitePawn.Move(oneHopWhitePawnPosition);
             board.SetFigure(whitePawn, oneHopWhitePawnPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(whitePawn, startWhitePawnPosition, oneHopWhitePawnPosition, null);
             var moveResult = new ValidMoveResult(board, verifier, null, lastMoveVm);

             var expectedLastMoveFigure = whitePawn;
             var expectedLastMovePositionFrom = startWhitePawnPosition;
             var expectedLastMovePositionDest = oneHopWhitePawnPosition;
             
             // act
             var result = moveResult.LastMoveFigureAndPositionFromAndDest(); 

             // assert
             Assert.AreEqual(expectedLastMoveFigure, result.Item1);
             Assert.AreEqual(expectedLastMovePositionFrom, result.Item2);
             Assert.AreEqual(expectedLastMovePositionDest, result.Item3);
         }
         
         [Test]
         public void IsCheckMate_NotCheck_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var pawnStartPosition = new Position(1, 0);
             var pawnEndPosition = new Position(2, 0);

             var pawn = board.RemoveFigure(pawnStartPosition);
             pawn.Move(pawnEndPosition);
             board.SetFigure(pawn,pawnEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(pawn, pawnStartPosition, pawnEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier, validator, lastMoveVm);

             var team1 = TeamColor.Black;
             var team2 = TeamColor.White;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.False(result1);
             Assert.False(result2);
             Assert.False(result3);
             Assert.False(result4);
         }
         
         [Test]
         public void IsCheckMate_OnlyCheck_KingMayEscape_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawn1StartPosition = new Position(1, 3);
             var blackPawn1EndPosition = new Position(2, 3);
             var blackPawn2StartPosition = new Position(1, 4);
             var blackPawn2EndPosition = new Position(2, 4);
             var whitePawnStartPosition = new Position(6, 2);
             var whitePawnEndPosition = new Position(5, 2);
             var whiteQueenStartPosition = new Position(7, 3);
             var whiteQueenEndPosition = new Position(4, 0);

             var blackPawn1 = board.RemoveFigure(blackPawn1StartPosition);
             blackPawn1.Move(blackPawn1EndPosition);
             board.SetFigure(blackPawn1,blackPawn1EndPosition);
             var blackPawn2 = board.RemoveFigure(blackPawn2StartPosition);
             blackPawn2.Move(blackPawn2EndPosition);
             board.SetFigure(blackPawn2,blackPawn2EndPosition);
             var whitePawn = board.RemoveFigure(whitePawnStartPosition);
             whitePawn.Move(whitePawnEndPosition);
             board.SetFigure(whitePawn,whitePawnEndPosition);
             var whiteQueen = board.RemoveFigure(whiteQueenStartPosition);
             whiteQueen.Move(whiteQueenEndPosition);
             board.SetFigure(whiteQueen,whiteQueenEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(whiteQueen, whiteQueenStartPosition, whiteQueenEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier, validator, lastMoveVm);

             var team1 = TeamColor.White;
             var team2 = TeamColor.Black;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.True(result1);
             Assert.False(result2);
             Assert.False(result3);
             Assert.False(result4);
         }
         
         [Test]
         public void IsCheckMate_OnlyCheck_CulpritMayBeKilled_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnStartPosition = new Position(1, 7);
             var blackPawnMiddlePosition = new Position(2, 7);
             var blackPawnEndPosition = new Position(3, 7);

             var whiteKnightStartPosition = new Position(7, 1);
             var whiteKnightFirstHopPosition = new Position(5, 2);
             var whiteKnightSecondHopPosition = new Position(3, 3);
             var whiteKnightEndPosition = new Position(2, 5);

             var blackPawn = board.RemoveFigure(blackPawnStartPosition);
             blackPawn.Move(blackPawnMiddlePosition);
             board.SetFigure(blackPawn,blackPawnMiddlePosition);
             board.RemoveFigure(blackPawnMiddlePosition);
             blackPawn.Move(blackPawnEndPosition);
             board.SetFigure(blackPawn,blackPawnEndPosition);
             var whiteKnight = board.RemoveFigure(whiteKnightStartPosition);
             whiteKnight.Move(whiteKnightFirstHopPosition);
             board.SetFigure(whiteKnight,whiteKnightFirstHopPosition);
             board.RemoveFigure(whiteKnightFirstHopPosition);
             whiteKnight.Move(whiteKnightSecondHopPosition);
             board.SetFigure(whiteKnight, whiteKnightSecondHopPosition);
             board.RemoveFigure(whiteKnightSecondHopPosition);
             whiteKnight.Move(whiteKnightEndPosition);
             board.SetFigure(whiteKnight, whiteKnightEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm =
                 new LastMoveViewModel(whiteKnight, whiteKnightSecondHopPosition, whiteKnightEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier, validator, lastMoveVm);

             var team1 = TeamColor.White;
             var team2 = TeamColor.Black;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.True(result1);
             Assert.False(result2);
             Assert.False(result3);
             Assert.False(result4);
         }
         
         [Test]
         public void IsCheckMate_OnlyCheck_PathMayBeBlocked_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnStartPosition = new Position(1, 3);
             var blackPawnEndPosition = new Position(2, 3);
             var whitePawnStartPosition = new Position(6, 2);
             var whitePawnEndPosition = new Position(5, 2);
             var whiteQueenStartPosition = new Position(7, 3);
             var whiteQueenEndPosition = new Position(4, 0);


             var blackPawn1 = board.RemoveFigure(blackPawnStartPosition);
             blackPawn1.Move(blackPawnEndPosition);
             board.SetFigure(blackPawn1,blackPawnEndPosition);
             var whitePawn = board.RemoveFigure(whitePawnStartPosition);
             whitePawn.Move(whitePawnEndPosition);
             board.SetFigure(whitePawn,whitePawnEndPosition);
             var whiteQueen = board.RemoveFigure(whiteQueenStartPosition);
             whiteQueen.Move(whiteQueenEndPosition);
             board.SetFigure(whiteQueen,whiteQueenEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(whiteQueen, whiteQueenStartPosition, whitePawnEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier, validator, lastMoveVm);

             var team1 = TeamColor.White;
             var team2 = TeamColor.Black;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.True(result1);
             Assert.False(result2);
             Assert.False(result3);
             Assert.False(result4);
         }
         
         [Test]
         public void IsCheckMate_Black_CheckMateBlackTeam()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawn1StartPosition = new Position(1, 5);
             var blackPawn1EndPosition = new Position(2, 5);
             var blackPawn2StartPosition = new Position(1, 6);
             var blackPawn2EndPosition = new Position(3, 6);
             var whitePawnStartPosition = new Position(6, 4);
             var whitePawnEndPosition = new Position(4, 4);
             var whiteQueenStartPosition = new Position(7, 3);
             var whiteQueenEndPosition = new Position(3, 7);

             var blackPawn1 = board.RemoveFigure(blackPawn1StartPosition);
             blackPawn1.Move(blackPawn1EndPosition);
             board.SetFigure(blackPawn1,blackPawn1EndPosition);
             var whitePawn = board.RemoveFigure(whitePawnStartPosition);
             whitePawn.Move(whitePawnEndPosition);
             board.SetFigure(whitePawn,whitePawnEndPosition);
             var blackPawn2 = board.RemoveFigure(blackPawn2StartPosition);
             blackPawn2.Move(blackPawn2EndPosition);
             board.SetFigure(blackPawn2,blackPawn2EndPosition);
             var whiteQueen = board.RemoveFigure(whiteQueenStartPosition);
             whiteQueen.Move(whiteQueenEndPosition);
             board.SetFigure(whiteQueen,whiteQueenEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(whiteQueen, whiteQueenStartPosition, whitePawnEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier,validator, lastMoveVm);

             var team1 = TeamColor.White;
             var team2 = TeamColor.Black;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.True(result1);
             Assert.False(result2);
             Assert.True(result3);
             Assert.False(result4);
         }
         
         [Test]
         public void IsCheckMate_CheckMateWhiteTeam()
         {
            // arrange
             var board = new OrdinaryChessBoard();

             var whitePawn1StartPosition = new Position(6, 5);
             var whitePawn1EndPosition = new Position(5, 5);
             var whitePawn2StartPosition = new Position(6, 6);
             var whitePawn2EndPosition = new Position(4, 6);
             var blackPawnStartPosition = new Position(1, 4);
             var blackPawnEndPosition = new Position(3, 4);
             var blackQueenStartPosition = new Position(0, 3);
             var blackQueenEndPosition = new Position(4, 7);

             var whitePawn1 = board.RemoveFigure(whitePawn1StartPosition);
             whitePawn1.Move(whitePawn1EndPosition);
             board.SetFigure(whitePawn1,whitePawn1EndPosition);
             var blackPawn = board.RemoveFigure(blackPawnStartPosition);
             blackPawn.Move(blackPawnEndPosition);
             board.SetFigure(blackPawn,blackPawnEndPosition);
             var whitePawn2 = board.RemoveFigure(whitePawn2StartPosition);
             whitePawn2.Move(whitePawn2EndPosition);
             board.SetFigure(whitePawn2,whitePawn2EndPosition);
             var blackQueen = board.RemoveFigure(blackQueenStartPosition);
             blackQueen.Move(blackQueenEndPosition);
             board.SetFigure(blackQueen,blackQueenEndPosition);
             
             var validator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, validator);
             var lastMoveVm = new LastMoveViewModel(blackQueen, blackQueenStartPosition, blackPawnEndPosition, null);
             var moveResult =
                 new ValidMoveResult(board, verifier, validator, lastMoveVm);

             var team1 = TeamColor.White;
             var team2 = TeamColor.Black;
             // act

             var result1 = moveResult.IsCheck(team1);
             var result2 = moveResult.IsCheck(team2);
             var result3 = moveResult.IsCheckMate(team1);
             var result4 = moveResult.IsCheckMate(team2);

             // assert
             Assert.False(result1);
             Assert.True(result2);
             Assert.False(result3);
             Assert.True(result4);
         }
    }
}