using System.Collections.Generic;
using System.Linq;
using Chess.Models.Position;
using NUnit.Framework;

namespace Chess.UnitTests.ModelsUnitTests.VectorUnitTests
{
    [TestFixture]
    public class VectorUnitTests
    {
        [Test]
        public void GetPath_Point_ShouldReturn_EmptyCollection()
        {
            // arrange
            var startPostion = new Models.Position.Position(1, 1);
            var endPostion = new Models.Position.Position(1, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Models.Position.Position> expected = new List<Models.Position.Position>();

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_HorizontalVector_PositiveShift()
        {
            // arrange
            var startPostion = new Models.Position.Position(1, 1);
            var endPostion = new Models.Position.Position(1, 4);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Models.Position.Position> expected = new List<Models.Position.Position>()
            {
                new Models.Position.Position(1,2),
                new Models.Position.Position(1,3),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_HorizontalVector_NegativeShift()
        {
            // arrange
            var startPostion = new Models.Position.Position(1, 4);
            var endPostion = new Models.Position.Position(1, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Models.Position.Position> expected = new List<Models.Position.Position>()
            {
                new Models.Position.Position(1,3),
                new Models.Position.Position(1,2),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_VerticalVector_PositiveShift()
        {
            // arrange
            var startPostion = new Models.Position.Position(1, 1);
            var endPostion = new Models.Position.Position(4, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Models.Position.Position> expected = new List<Models.Position.Position>()
            {
                new Models.Position.Position(2,1),
                new Models.Position.Position(3,1),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_VerticalVector_NegativeShift()
        {
            // arrange
            var startPostion = new Models.Position.Position(4, 1);
            var endPostion = new Models.Position.Position(1, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Models.Position.Position> expected = new List<Models.Position.Position>()
            {
                new Models.Position.Position(3,1),
                new Models.Position.Position(2,1),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_DiagonalVector_PositiveShiftX_PositiveShiftY()
        {
            // arrange
            var startPostion = new Position(1, 1);
            var endPostion = new Position(4, 4);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Position> expected = new List<Position>()
            {
                new Position(2,2),
                new Position(3,3),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_DiagonalVector_NegativeShiftX_NegativeShiftY()
        {
            // arrange
            var startPostion = new Position(4, 4);
            var endPostion = new Position(1, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Position> expected = new List<Position>()
            {
                new Position(3,3),
                new Position(2,2),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_DiagonalVector_PositiveShiftX_NegativeShiftY()
        {
            // arrange
            var startPostion = new Position(4, 1);
            var endPostion = new Position(1, 4);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Position> expected = new List<Position>()
            {
                new Position(3,2),
                new Position(2,3),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void GetPath_DiagonalVector_NegativeShiftX_PositiveShiftY()
        {
            // arrange
            var startPostion = new Position(1, 4);
            var endPostion = new Position(4, 1);
            var vector = new Vector(startPostion, endPostion);
            IEnumerable<Position> expected = new List<Position>()
            {
                new Position(2,3),
                new Position(3,2),
            };

            // act
            var result = vector.GetPath();

            // assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}