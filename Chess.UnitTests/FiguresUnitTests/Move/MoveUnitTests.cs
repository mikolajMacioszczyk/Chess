using Chess.Figures;
using Chess.Figures.FigureImplementation;
using Chess.GameManager;
using Chess.Team;
using Moq;
using NUnit.Framework;

namespace Chess.UnitTests.FiguresUnitTests.Move
{
    [TestFixture]
    public class MoveUnitTests
    {
        [Test]
        public void King_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void King_CanMove_HorizontalMoveValid_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black, null);
            Position.Position destinationPosition = new Position.Position(1, 0);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void King_CanMove_HorizontalMoveInvalid_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black, null);
            Position.Position destinationPosition = new Position.Position(2, 0);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void King_CanMove_DiagonalMoveValid_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black, null);
            Position.Position destinationPosition = new Position.Position(1, 1);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void King_CanMove_DiagonalMoveInvalid_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black, null);
            Position.Position destinationPosition = new Position.Position(2, 2);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Queen_CanMove_ThisSamePosition_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0, 0);
            var queen = new Queen(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = queen.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Queen_CanMove_HorizontalAndVerticalMove_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition1 = new Position.Position(3, 0);
            Position.Position destinationPosition2 = new Position.Position(0, 5);
            var queen = new Queen(startingPosition, TeamColor.Black, null);
            
            // act
            bool result1 = queen.CanMove(destinationPosition1);
            bool result2 = queen.CanMove(destinationPosition2);
            
            // assert
            Assert.True(result1);
            Assert.True(result2);
        }
        
        [Test]
        public void Queen_CanMove_DiagonalMove_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition1 = new Position.Position(3, 3);
            Position.Position destinationPosition2 = new Position.Position(-2, 2);
            var queen = new Queen(startingPosition, TeamColor.Black, null);
            
            // act
            bool result1 = queen.CanMove(destinationPosition1);
            bool result2 = queen.CanMove(destinationPosition2);
            
            // assert
            Assert.True(result1);
            Assert.True(result2);
        }
        
        [Test]
        public void Queen_CanMove_InvalidMove_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(3, 2);
            var queen = new Queen(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = queen.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0, 0);
            var rook = new Rook(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_ValidMoveHorizontal_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(2, 0);
            var rook = new Rook(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Rook_CanMove_ValidMoveVertical_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0, 2);
            var rook = new Rook(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Rook_CanMove_InvalidMoveDiagonal_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(1, 1);
            var rook = new Rook(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_InvalidMove_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(1, 2);
            var rook = new Rook(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0, 0);
            var bishop = new Bishop(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_ValidMoveTopRight_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(2, 1);
            var bishop = new Bishop(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Bishop_CanMove_ValidMoveBottomLeft_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(1, 2);
            Position.Position destinationPosition = new Position.Position(0,0);
            var bishop = new Bishop(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Bishop_CanMove_InvalidMoveDiagonal_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(2,2);
            var bishop = new Bishop(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_InvalidMoveHorizontal_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(2,0);
            var bishop = new Bishop(startingPosition, TeamColor.Black, null);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_ThisSamePosition_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0,0);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_UpBlack_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0,1);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_DownBlack_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 1);
            Position.Position destinationPosition = new Position.Position(0,0);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_UpWhite_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0,1);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.White, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_DownWhite_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 1);
            Position.Position destinationPosition = new Position.Position(0,0);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.White, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_FirstMoveTwoFieldsForward_FirstMove_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(0,2);
            var mockGameManager = new Mock<IGameManager>();
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_FirstMoveTwoFieldsForward_SecondMove_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position intermediatePosition = new Position.Position(0,1);
            Position.Position destinationPosition = new Position.Position(0,3);
            var mockGameManager = new Mock<IGameManager>();
            mockGameManager.Setup(m => m.CanMove(It.IsAny<Figure>(), intermediatePosition)).Returns(true);
            mockGameManager.Setup(m => m.CanMove(It.IsAny<Figure>(), destinationPosition)).Returns(true);
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result1 = pawn.CanMove(intermediatePosition);
            pawn.Move(intermediatePosition);
            bool result2 = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Test] public void Pawn_CanMove_DiagonalTwoFields_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(2,2);
            var mockGameManager = new Mock<IGameManager>();
            mockGameManager.Setup(m => m.IsEnemyAtPosition(destinationPosition, TeamColor.Black)).Returns(true);
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_DiagonalOneFields_EnemyOnDiagonal_ShouldReturnTrue()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(1,1);
            var mockGameManager = new Mock<IGameManager>();
            mockGameManager.Setup(m => m.IsEnemyAtPosition(destinationPosition, TeamColor.Black)).Returns(true);
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_DiagonalOneFields_EnemyNotOnDiagonal_ShouldReturnFalse()
        {
            // arrange
            Position.Position startingPosition = new Position.Position(0, 0);
            Position.Position destinationPosition = new Position.Position(1,1);
            var mockGameManager = new Mock<IGameManager>();
            mockGameManager.Setup(m => m.IsEnemyAtPosition(destinationPosition, TeamColor.Black)).Returns(false);
            var pawn = new Pawn(startingPosition, TeamColor.Black, mockGameManager.Object);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
    }
}