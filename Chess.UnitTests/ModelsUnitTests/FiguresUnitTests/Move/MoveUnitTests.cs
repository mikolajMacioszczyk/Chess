using Chess.Game.Team;
using Chess.Models.Figures.FigureImplementation;
using NUnit.Framework;

namespace Chess.UnitTests.ModelsUnitTests.FiguresUnitTests.Move
{
    [TestFixture]
    public class MoveUnitTests
    {
        [Test]
        public void King_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void King_CanMove_HorizontalMoveValid_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black);
            Models.Position.Position destinationPosition = new Models.Position.Position(1, 0);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void King_CanMove_HorizontalMoveInvalid_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 0);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void King_CanMove_DiagonalMoveValid_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black);
            Models.Position.Position destinationPosition = new Models.Position.Position(1, 1);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void King_CanMove_DiagonalMoveInvalid_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            var king = new King(startingPosition, TeamColor.Black);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 2);
            
            // act
            bool result = king.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Queen_CanMove_ThisSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 0);
            var queen = new Queen(startingPosition, TeamColor.Black);
            
            // act
            bool result = queen.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Queen_CanMove_HorizontalAndVerticalMove_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition1 = new Models.Position.Position(3, 0);
            Models.Position.Position destinationPosition2 = new Models.Position.Position(0, 5);
            var queen = new Queen(startingPosition, TeamColor.Black);
            
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
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition1 = new Models.Position.Position(3, 3);
            Models.Position.Position destinationPosition2 = new Models.Position.Position(-2, 2);
            var queen = new Queen(startingPosition, TeamColor.Black);
            
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
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(3, 2);
            var queen = new Queen(startingPosition, TeamColor.Black);
            
            // act
            bool result = queen.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 0);
            var rook = new Rook(startingPosition, TeamColor.Black);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_ValidMoveHorizontal_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 0);
            var rook = new Rook(startingPosition, TeamColor.Black);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Rook_CanMove_ValidMoveVertical_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 2);
            var rook = new Rook(startingPosition, TeamColor.Black);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Rook_CanMove_InvalidMoveDiagonal_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(1, 1);
            var rook = new Rook(startingPosition, TeamColor.Black);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Rook_CanMove_InvalidMove_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(1, 2);
            var rook = new Rook(startingPosition, TeamColor.Black);
            
            // act
            bool result = rook.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 0);
            var bishop = new Bishop(startingPosition, TeamColor.Black);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_DiagonalMove_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 2);
            var bishop = new Bishop(startingPosition, TeamColor.Black);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Bishop_CanMove_HorizontalMove_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 0);
            var bishop = new Bishop(startingPosition, TeamColor.Black);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Bishop_CanMove_VerticalMove_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 2);
            var bishop = new Bishop(startingPosition, TeamColor.Black);
            
            // act
            bool result = bishop.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Knight_CanMove_TheSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0, 0);
            var knight = new Knight(startingPosition, TeamColor.Black);
            
            // act
            bool result = knight.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Knight_CanMove_ValidMoveTopRight_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2, 1);
            var knight = new Knight(startingPosition, TeamColor.Black);
            
            // act
            bool result = knight.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Knight_CanMove_ValidMoveBottomLeft_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(1, 2);
            Models.Position.Position destinationPosition = new Models.Position.Position(0,0);
            var knight = new Knight(startingPosition, TeamColor.Black);
            
            // act
            bool result = knight.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Knight_CanMove_InvalidMoveDiagonal_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2,2);
            var knight = new Knight(startingPosition, TeamColor.Black);
            
            // act
            bool result = knight.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Knight_CanMove_InvalidMoveHorizontal_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2,0);
            var knight = new Knight(startingPosition, TeamColor.Black);
            
            // act
            bool result = knight.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_ThisSamePosition_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0,0);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_UpBlack_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(1,0);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_DownBlack_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 1);
            Models.Position.Position destinationPosition = new Models.Position.Position(0,0);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_UpWhite_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0,1);
            var pawn = new Pawn(startingPosition, TeamColor.White);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
        
        [Test]
        public void Pawn_CanMove_DownWhite_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(1, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(0,0);
            var pawn = new Pawn(startingPosition, TeamColor.White);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_FirstMoveTwoFieldsForward_FirstMove_ShouldReturnTrue()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2,0);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.True(result);
        }
        
        [Test]
        public void Pawn_CanMove_FirstMoveTwoFieldsForward_SecondMove_ShouldReturnFalse()
        {
            // arrange
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position intermediatePosition = new Models.Position.Position(1,0);
            Models.Position.Position destinationPosition = new Models.Position.Position(3,0);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
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
            Models.Position.Position startingPosition = new Models.Position.Position(0, 0);
            Models.Position.Position destinationPosition = new Models.Position.Position(2,2);
            var pawn = new Pawn(startingPosition, TeamColor.Black);
            
            // act
            bool result = pawn.CanMove(destinationPosition);
            
            // assert
            Assert.False(result);
        }
    }
}