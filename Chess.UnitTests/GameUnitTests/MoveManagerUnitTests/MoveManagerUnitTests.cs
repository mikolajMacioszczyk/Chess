using Chess.Game.MoveManager;
using Chess.Models.Position;
using NUnit.Framework;

namespace Chess.UnitTests.GameUnitTests.MoveManagerUnitTests
{
    [TestFixture]
    public class MoveManagerUnitTests
    {
        [Test]
        public void CanMove_ValidPosition_ShouldReturnTrue()
        {
            // arrange
            var gameManger = new MoveManager();

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
            var gameManger = new MoveManager();

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
            var gameManger = new MoveManager();

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
            var gameManger = new MoveManager();
            
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

        [Test]
        public void Undo_CanUndo_OrdinaryMove_ShouldReturnTrue()
        {
            // arrange
            var moveManager = new MoveManager();
            Position from = new Position(1, 1);
            Position dest = new Position(2, 1);
            moveManager.Move(from, dest);

            // act
            bool before = moveManager.CanMove(from, dest);
            bool result = moveManager.Undo();
            bool after = moveManager.CanMove(from, dest);

            // assert
            Assert.False(before);
            Assert.True(result);
            Assert.True(after);
        }

        [Test]
        public void Undo_CanUndo_Kill_ShouldReturnTrue()
        {
            // arrange
            var moveManager = new MoveManager();
            moveManager.Move(new Position(1,1), new Position(2,1));
            moveManager.Move(new Position(6, 4), new Position(5, 4));
            Position from = new Position(7, 5);
            Position dest = new Position(2, 0);
            moveManager.Move(new Position(0, 2), dest);
            moveManager.Move(from, dest);

            // act
            bool before = moveManager.CanMove(from, dest);
            bool result = moveManager.Undo();
            bool after = moveManager.CanMove(from, dest);
            bool afterKilled = moveManager.CanMove(dest, from);

            // assert
            Assert.False(before);
            Assert.True(result);
            Assert.True(after);
            Assert.True(afterKilled);
        }

        [Test]
        public void Undo_CanNotUndo_ShouldReturnFalse()
        {
            // arrange
            var moveManager = new MoveManager();
            Position from = new Position(1, 1);
            Position mid = new Position(2, 1);
            Position dest = new Position(3, 1);
            moveManager.Move(from, mid);
            moveManager.Move(mid, dest);

            // act
            bool firstUndo = moveManager.Undo();
            bool secondUndo = moveManager.Undo();
            bool canMoveValid = moveManager.CanMove(mid, dest);
            bool canMoveInvalid = moveManager.CanMove(from, mid);

            // assert
            Assert.True(firstUndo);
            Assert.False(secondUndo);
            Assert.True(canMoveValid);
            Assert.False(canMoveInvalid);
        }
    }
}