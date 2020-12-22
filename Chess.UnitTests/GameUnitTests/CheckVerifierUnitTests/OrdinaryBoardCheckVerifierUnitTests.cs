using Chess.Exceptions.InvalidBoardActionException;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.Models.Board;
using Chess.Models.Position;
using NUnit.Framework;

namespace Chess.UnitTests.GameUnitTests.CheckVerifierUnitTests
{
    [TestFixture]
    public class OrdinaryBoardCheckVerifierUnitTests
    {
        [Test]
        public void IsCheck_BlackTeam_NotCheck_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var startBlackPawnPosition = new Position(1, 5);
             var endBlackPawnPosition = new Position(3, 5);

             var blackPawn = board.RemoveFigure(startBlackPawnPosition);
             blackPawn.Move(endBlackPawnPosition);
             board.SetFigure(blackPawn, endBlackPawnPosition);
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var checkVerifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.Black;
             
             // act
             var result = checkVerifier.IsCheck(teamColor);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void IsCheck_BlackTeam_Check_Bishop_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var blackPawnDeletePosition = new Position(1, 3);
             var whiteBishopStartPosition = new Position(7, 5);
             var whiteBishopEndPosition = new Position(4, 0);
             
             board.RemoveFigure(blackPawnDeletePosition);
             var bishop = board.RemoveFigure(whiteBishopStartPosition);
             bishop.Move(whiteBishopEndPosition);
             board.SetFigure(bishop,whiteBishopEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.Black;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void IsCheck_WhiteTeam_BlackChecked_Bishop_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var blackPawnDeletePosition = new Position(1, 3);
             var whiteBishopStartPosition = new Position(7, 5);
             var whiteBishopEndPosition = new Position(4, 0);
             
             board.RemoveFigure(blackPawnDeletePosition);
             var bishop = board.RemoveFigure(whiteBishopStartPosition);
             bishop.Move(whiteBishopEndPosition);
             board.SetFigure(bishop,whiteBishopEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.White;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void IsCheck_WhiteTeam_Check_Bishop_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var pawnDeletePosition = new Position(6, 3);
             var bishopStartPosition = new Position(0, 5);
             var bishopEndPosition = new Position(3, 0);
             
             board.RemoveFigure(pawnDeletePosition);
             var bishop = board.RemoveFigure(bishopStartPosition);
             bishop.Move(bishopEndPosition);
             board.SetFigure(bishop,bishopEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.White;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.True(result);
         }

         [Test] public void IsCheck_BlackTeam_WhiteChecked_Bishop_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var pawnDeletePosition = new Position(6, 3);
             var bishopStartPosition = new Position(0, 5);
             var bishopEndPosition = new Position(3, 0);
             
             board.RemoveFigure(pawnDeletePosition);
             var bishop = board.RemoveFigure(bishopStartPosition);
             bishop.Move(bishopEndPosition);
             board.SetFigure(bishop,bishopEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.Black;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void IsCheck_BlackTeam_Check_Knight_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(7, 1);
             var knightEndPosition = new Position(2, 3);
             
             var knight = board.RemoveFigure(knightStartPosition);
             knight.Move(knightEndPosition);
             board.SetFigure(knight, knightEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.Black;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void IsCheck_WhiteTeam_BlackChecked_Knight_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(7, 1);
             var knightEndPosition = new Position(2, 3);
             
             var knight = board.RemoveFigure(knightStartPosition);
             knight.Move(knightEndPosition);
             board.SetFigure(knight, knightEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.White;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void IsCheck_WhiteTeam_Check_Knight_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(0, 1);
             var knightEndPosition = new Position(5, 3);
             
             var knight = board.RemoveFigure(knightStartPosition);
             knight.Move(knightEndPosition);
             board.SetFigure(knight, knightEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.White;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.True(result);
         }

         [Test] public void IsCheck_BlackTeam_WhiteChecked_Knight_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(0, 1);
             var knightEndPosition = new Position(5, 3);
             
             var knight = board.RemoveFigure(knightStartPosition);
             knight.Move(knightEndPosition);
             board.SetFigure(knight, knightEndPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             TeamColor teamColor = TeamColor.Black;
             
             // act
             var result = verifier.IsCheck(teamColor);
             
             // assert
             Assert.False(result);
         }


         [Test]
         public void VerifyMoveCauseCheck_MoveNotCauseCheck_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var pawnStartPosition = new Position(1, 0);
             var pawnEndPosition = new Position(2, 0);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(pawnStartPosition, pawnEndPosition);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_NoFigureAtPosition_ShouldThrowException()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var pawnStartPosition = new Position(2, 0);
             var pawnEndPosition = new Position(3, 0);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             
             // assert
             Assert.Throws<RemoveFromBoardEmptyFieldException>(() =>
                 verifier.VerifyMoveCauseCheck(pawnStartPosition, pawnEndPosition));
         }
         
         [Test]
         public void VerifyMoveCauseCheck_CannotMove_InvalidMoveForFigure_ShouldThrowException()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(0, 1);
             var knightEndPosition = new Position(5, 1);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             
             // assert
             Assert.Throws<InvalidMoveException>(() =>
                 verifier.VerifyMoveCauseCheck(knightStartPosition, knightEndPosition));
         }
         
         [Test]
         public void VerifyMoveCauseCheck_CannotMove_Rook_OtherFigureBlock_ShouldThrowException()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var rookStartPosition = new Position(0, 0);
             var rookEndPosition = new Position(2, 0);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             
             // assert
             Assert.Throws<InvalidMoveException>(() =>
                 verifier.VerifyMoveCauseCheck(rookStartPosition, rookEndPosition));
         }
         
         [Test]
         public void VerifyMoveCauseCheck_Knight_OtherFigureBlock_OK_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard(); 
             var knightStartPosition = new Position(0, 1);
             var knightEndPosition = new Position(2, 0);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(knightStartPosition, knightEndPosition);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_MoveBlackBishop_CheckWhiteKing_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var whiteRemovePawnPosition = new Position(6, 3);
             var blackRemovePawnPosition = new Position(1,4);
             var blackBishopStartPosition = new Position(0, 5);
             var blackBishopEndPosition = new Position(4, 1);

             board.RemoveFigure(whiteRemovePawnPosition);
             board.RemoveFigure(blackRemovePawnPosition);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(blackBishopStartPosition, blackBishopEndPosition);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_MoveWhiteKnight_CheckBlackKing_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();
             var whiteKnightStartPosition = new Position(7, 1);
             var whiteKnightMiddlePosition = new Position(4, 2);
             var whiteKnightEndPosition = new Position(2, 3);

             var knight = board.RemoveFigure(whiteKnightStartPosition);
             knight.Move(whiteKnightMiddlePosition);
             board.SetFigure(knight, whiteKnightMiddlePosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(whiteKnightMiddlePosition, whiteKnightEndPosition);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_BlackKingMove_MoveToCheckedPosition_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnRemovePosition = new Position(1, 4);
             var startBlackKingPosition = new Position(0, 4);
             var endBlackKingPosition = new Position(1, 4);
             var startWhiteQueenPosition = new Position(7, 3);
             var endWhiteQueenPosition = new Position(5, 0);

             board.RemoveFigure(blackPawnRemovePosition);
             var whiteQueen = board.RemoveFigure(startWhiteQueenPosition);
             whiteQueen.Move(endWhiteQueenPosition);
             board.SetFigure(whiteQueen, endWhiteQueenPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(startBlackKingPosition, endBlackKingPosition);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_BlackPawn_RevealsTheBlackKing_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnStartPosition = new Position(1, 3);
             var blackPawnEndPosition = new Position(2, 3);
             var startWhiteQueenPosition = new Position(7, 3);
             var endWhiteQueenPosition = new Position(4, 0);

             var whiteQueen = board.RemoveFigure(startWhiteQueenPosition);
             whiteQueen.Move(endWhiteQueenPosition);
             board.SetFigure(whiteQueen, endWhiteQueenPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(blackPawnStartPosition, blackPawnEndPosition);
             
             // assert
             Assert.True(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_BlackKingChecked_MoveToSavePosition_ShouldReturnFalse()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnRemove1Postion = new Position(1, 3);
             var blackPawnRemove2Postion = new Position(1, 4);
             var startWhiteQueenPosition = new Position(7, 3);
             var endWhiteQueenPosition = new Position(4, 0);
             var blackKingStartPosition = new Position(0, 4);
             var blackKingEndPosition = new Position(1, 4);

             board.RemoveFigure(blackPawnRemove1Postion);
             board.RemoveFigure(blackPawnRemove2Postion);

             var whiteQueen = board.RemoveFigure(startWhiteQueenPosition);
             whiteQueen.Move(endWhiteQueenPosition);
             board.SetFigure(whiteQueen, endWhiteQueenPosition);

             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(blackKingStartPosition, blackKingEndPosition);
             
             // assert
             Assert.False(result);
         }
         
         [Test]
         public void VerifyMoveCauseCheck_BlackKingChecked_MoveToCheckedPosition_ShouldReturnTrue()
         {
             // arrange
             var board = new OrdinaryChessBoard();

             var blackPawnRemove1Postion = new Position(1, 3);
             var blackPawnRemove2Postion = new Position(1, 4);
             var startWhiteQueenPosition = new Position(7, 3);
             var endWhiteQueenPosition = new Position(4, 0);
             var whiteBishopStartPosition = new Position(7, 2);
             var whiteBishopEndPosition = new Position(4, 1);
             var blackKingStartPosition = new Position(0, 4);
             var blackKingEndPosition = new Position(1, 4);

             board.RemoveFigure(blackPawnRemove1Postion);
             board.RemoveFigure(blackPawnRemove2Postion);

             var whiteQueen = board.RemoveFigure(startWhiteQueenPosition);
             whiteQueen.Move(endWhiteQueenPosition);
             board.SetFigure(whiteQueen, endWhiteQueenPosition);

             var bishop = board.RemoveFigure(whiteBishopStartPosition);
             bishop.Move(whiteBishopEndPosition);
             board.SetFigure(bishop, whiteBishopEndPosition);
             
             var moveValidator = new OrdinaryBoardMoveValidator(board);
             var verifier = new OrdinaryBoardCheckVerifier(board, moveValidator);

             // act
             var result = verifier.VerifyMoveCauseCheck(blackKingStartPosition, blackKingEndPosition);
             
             // assert
             Assert.True(result);
         }
    }
}