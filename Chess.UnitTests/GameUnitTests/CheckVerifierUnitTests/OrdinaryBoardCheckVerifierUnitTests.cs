using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
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
         public void IsCheck_WhiteTeam_BlackChecked_Knight_ShouldReturnFlase()
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
    }
}