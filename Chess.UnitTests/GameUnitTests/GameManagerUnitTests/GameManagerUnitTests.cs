using Chess.Game.GameManager;
using Chess.Models.Position;
using NUnit.Framework;

namespace Chess.UnitTests.GameUnitTests.GameManagerUnitTests
{
    [TestFixture]
    public class GameManagerUnitTests
    {
        [Test]
        public void CanMove_ValidPosition_ShouldReturnTrue()
        {
            // arrange
            var gameManger = new GameManager();

            var startPawnPosition = new Position(1, 0);
            var endPawnPosition = new Position(3, 0);
            // act

            var result = gameManger.CanMove(startPawnPosition, endPawnPosition);

            // assert
            Assert.True(result);
        }

        [Test]
        public void CanMove_NotAllowedMoveForThisTypeOfFigure_ShouldReturnFalse()
        {
            // arrange
            var gameManger = new GameManager();

            var startPawnPosition = new Position(1, 0);
            var endPawnPosition = new Position(3, 2);
            // act

            var result = gameManger.CanMove(startPawnPosition, endPawnPosition);

            // assert
            Assert.False(result);
        }

        [Test]
        public void CanMove_PathBlockedByOtherFigure_ShouldReturnFalse()
        {
            // arrange
            var gameManger = new GameManager();

            var startRookPosition = new Position(0, 0);
            var endRookPosition = new Position(3, 0);
            // act

            var result = gameManger.CanMove(startRookPosition, endRookPosition);

            // assert
            Assert.False(result);
        }

        [Test]
        public void CanMove_MoveCauseCheck_ShouldReturnFalse()
        {
            // arrange
            var gameManger = new GameManager();
            
            var whitePawnStart = new Position(6, 4);
            var whitePawnEnd = new Position(4, 4);
            var blackPawn2Start = new Position(1, 6);
            var blackPawn2End = new Position(3, 6);
            var whiteQueenStart = new Position(7, 3);
            var whiteQueenEnd = new Position(3, 7);

            gameManger.Move(whitePawnStart, whitePawnEnd);
            gameManger.Move(blackPawn2Start, blackPawn2End);
            gameManger.Move(whiteQueenStart, whiteQueenEnd);
            
            var blackPawn1Start = new Position(1, 5);
            var blackPawn1End = new Position(2, 5);
            
            // act
            var result = gameManger.CanMove(blackPawn1Start, blackPawn1End);

            // assert
            Assert.False(result);
        }
    }
}