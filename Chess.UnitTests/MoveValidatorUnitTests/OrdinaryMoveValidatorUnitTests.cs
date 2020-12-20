using Chess.Board;
using Chess.MoveValidator;
using NUnit.Framework;

namespace Chess.UnitTests.MoveValidatorUnitTests
{
    [TestFixture]
    public class OrdinaryMoveValidatorUnitTests
    {
        [Test]
        public void CanMove_King_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var destPosition = new Position.Position(1, 4);
            var kingPosition = new Position.Position(0, 4);
            board.RemoveFigure(destPosition);
            var king = board.FigureAt(kingPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(king, destPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_King_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var destPosition = new Position.Position(-1, 4);
            var kingPosition = new Position.Position(0, 4);
            var king = board.FigureAt(kingPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(king, destPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_King_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var destPosition = new Position.Position(1, 4);
            var kingPosition = new Position.Position(0, 4);
            var king = board.FigureAt(kingPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(king, destPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_King_PositionAlreadyTakenByEnemy_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var destPosition = new Position.Position(1, 4);
            var startEnemyPosition = new Position.Position(6, 4);
            var kingPosition = new Position.Position(0, 4);

            board.RemoveFigure(destPosition);
            var enemyPawn = board.RemoveFigure(startEnemyPosition);
            enemyPawn.Move(destPosition);
            board.SetFigure(enemyPawn, destPosition);
            
            var king = board.FigureAt(kingPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(king, destPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Queen_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(4, 7);
            var pawnPosition = new Position.Position(1, 4);
            board.RemoveFigure(pawnPosition);
            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Queen_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(4, 8);
            var pawnPosition = new Position.Position(1, 4);
            board.RemoveFigure(pawnPosition);
            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Queen_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(1, 4);
            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Queen_PositionAlreadyTakenByEnemy_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(4, 7);
            var pawnPosition = new Position.Position(1, 4);
            var enemyPawnPosition = new Position.Position(6, 7);

            board.RemoveFigure(pawnPosition);
            var enemyPawn = board.RemoveFigure(enemyPawnPosition);
            enemyPawn.Move(destQueenPosition);
            board.SetFigure(enemyPawn, destQueenPosition);
            
            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Queen_AllyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(4, 7);

            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Queen_EnemyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startQueenPosition = new Position.Position(0, 3);
            var destQueenPosition = new Position.Position(4, 7);
            var pawnPosition = new Position.Position(1, 4);
            var enemyPawnStartPosition = new Position.Position(6, 6);
            var enemyPawnEndPosition = new Position.Position(3, 6);

            board.RemoveFigure(pawnPosition);
            var enemyPawn = board.RemoveFigure(enemyPawnStartPosition);
            enemyPawn.Move(enemyPawnEndPosition);
            board.SetFigure(enemyPawn, enemyPawnEndPosition);
            
            var queen = board.FigureAt(startQueenPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destQueenPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Rook_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(4, 0);
            var pawnPosition = new Position.Position(1, 0);
            board.RemoveFigure(pawnPosition);
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Rook_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(-1, 0);
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Rook_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(4, 0);
            var pawnPosition = new Position.Position(1, 0);
            
            var pawn = board.RemoveFigure(pawnPosition);
            pawn.Move(destRookPosition);
            board.SetFigure(pawn, destRookPosition);
            
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Rook_PositionAlreadyTakenByEnemy_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(6, 0);
            var pawnPosition = new Position.Position(1, 0);
            
            board.RemoveFigure(pawnPosition);
            
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Rook_AllyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(4, 0);
            
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Rook_EnemyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startRookPosition = new Position.Position(0, 0);
            var destRookPosition = new Position.Position(7, 0);
            var pawnPosition = new Position.Position(1, 0);
            
            board.RemoveFigure(pawnPosition);
            
            var queen = board.FigureAt(startRookPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(queen, destRookPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Bishop_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(5, 7);
            var pawnPosition = new Position.Position(1, 3);
            
            board.RemoveFigure(pawnPosition);
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Bishop_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(5, 8);
            var pawnPosition = new Position.Position(1, 3);
            
            board.RemoveFigure(pawnPosition);
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Bishop_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(1, 3);
            
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Bishop_PositionAlreadyTakenByEnemy_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(5, 7);
            var pawnPosition = new Position.Position(1, 3);
            var enemyPawnPosition = new Position.Position(6, 7);
            
            board.RemoveFigure(pawnPosition);
            var enemyPawn = board.RemoveFigure(enemyPawnPosition);
            enemyPawn.Move(destBishopPosition);
            board.SetFigure(enemyPawn, destBishopPosition);
            
            
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Bishop_AllyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(5, 7);
            
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Bishop_EnemyBlockPath_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startBishopPosition = new Position.Position(0, 2);
            var destBishopPosition = new Position.Position(5, 7);
            var pawnPosition = new Position.Position(1, 3);
            var startEnemyPawnPosition = new Position.Position(6, 6);
            var destEnemyPawnPosition = new Position.Position(4, 6);
            
            board.RemoveFigure(pawnPosition);
            var enemyPawn = board.RemoveFigure(startEnemyPawnPosition);
            enemyPawn.Move(destEnemyPawnPosition);
            board.SetFigure(enemyPawn, destEnemyPawnPosition);
            
            
            var bishop = board.FigureAt(startBishopPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destBishopPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Knight_AllyBlockPath_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Position.Position(0, 1);
            var destKnightPosition = new Position.Position(2, 2);

            var bishop = board.FigureAt(startKnightPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destKnightPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Knight_EnemyBlockPath_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Position.Position(0, 1);
            var destKnightPosition = new Position.Position(2, 2);
            var enemyPawnStartPosition = new Position.Position(6, 1);
            var enemyPawnEndPosition = new Position.Position(2, 1);

            var enemy = board.RemoveFigure(enemyPawnStartPosition);
            enemy.Move(enemyPawnEndPosition);
            board.SetFigure(enemy, enemyPawnEndPosition);

            var bishop = board.FigureAt(startKnightPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destKnightPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Knight_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Position.Position(0, 1);
            var destKnightPosition = new Position.Position(-1, 3);

            var bishop = board.FigureAt(startKnightPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destKnightPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Knight_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Position.Position(0, 1);
            var destKnightPosition = new Position.Position(1, 3);

            var bishop = board.FigureAt(startKnightPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destKnightPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Knight_PositionAlreadyTakenByEnemy_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startKnightPosition = new Position.Position(0, 1);
            var destKnightPosition = new Position.Position(2, 2);
            var enemyPawnStartPosition = new Position.Position(6, 2);
            var enemyPawnEndPosition = new Position.Position(2, 2);

            var enemy = board.RemoveFigure(enemyPawnStartPosition);
            enemy.Move(enemyPawnEndPosition);
            board.SetFigure(enemy, enemyPawnEndPosition);

            var bishop = board.FigureAt(startKnightPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(bishop, destKnightPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Pawn_CanMove_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startPawnPosition = new Position.Position(1, 0);
            var destPawnPosition = new Position.Position(2, 0);
            
            var pawn = board.FigureAt(startPawnPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(pawn, destPawnPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Pawn_OutOfBoundaries_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startPawnPosition = new Position.Position(1, 0);
            var destPawnPosition = new Position.Position(2, -1);
            
            var pawn = board.FigureAt(startPawnPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(pawn, destPawnPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Pawn_PositionAlreadyTakenByAlly_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startPawnPosition = new Position.Position(1, 0);
            var destPawnPosition = new Position.Position(2, 0);
            var allyStartPosition = new Position.Position(1, 1);

            var ally = board.RemoveFigure(allyStartPosition);
            ally.Move(destPawnPosition);
            board.SetFigure(ally, destPawnPosition);
            
            var pawn = board.FigureAt(startPawnPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(pawn, destPawnPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void CanMove_Pawn_PositionAlreadyTakenByEnemy_Attack_ShouldReturnTrue()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startPawnPosition = new Position.Position(1, 0);
            var destPawnPosition = new Position.Position(2, 0);
            var enemyStartPosition = new Position.Position(6, 1);
            var enemyEndPosition = new Position.Position(2, 1);

            var enemy = board.RemoveFigure(enemyStartPosition);
            enemy.Move(enemyEndPosition);
            board.SetFigure(enemy,enemyEndPosition);
            
            var pawn = board.FigureAt(startPawnPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(pawn, destPawnPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void CanMove_Pawn_PositionAlreadyTakenByEnemy_NotAttack_ShouldReturnFalse()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var startPawnPosition = new Position.Position(1, 0);
            var destPawnPosition = new Position.Position(2, 0);
            var enemyStartPosition = new Position.Position(6, 0);

            var enemy = board.RemoveFigure(enemyStartPosition);
            enemy.Move(destPawnPosition);
            board.SetFigure(enemy,destPawnPosition);
            
            var pawn = board.FigureAt(startPawnPosition);
            var validator = new OrdinaryBoardMoveValidator(board);
            
            // act
            var result = validator.CanMove(pawn, destPawnPosition);
            
            // assert
            Assert.False(result);
        }
    }
}