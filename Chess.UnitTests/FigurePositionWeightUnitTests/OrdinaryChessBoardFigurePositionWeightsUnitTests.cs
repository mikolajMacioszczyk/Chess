using Chess.FigurePositionWeight;
using Chess.Figures;
using Chess.Figures.FigureImplementation;
using Chess.Team;
using NUnit.Framework;

namespace Chess.UnitTests.FigurePositionWeightUnitTests
{
    [TestFixture]
    public class OrdinaryChessBoardFigurePositionWeightsUnitTests
    {
        [Test]
        public void GetFigureTypeWeight_King_ShouldReturn1800()
        {
            // arrange
            var figureType = FigureType.King;
            int expected = 1800;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetFigureTypeWeight(figureType);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetFigureTypeWeight_Queen_ShouldReturn180()
        {
            // arrange
            var figureType = FigureType.Queen;
            int expected = 180;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetFigureTypeWeight(figureType);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetFigureTypeWeight_Rook_ShouldReturn100()
        {
            // arrange
            var figureType = FigureType.Rook;
            int expected = 100;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetFigureTypeWeight(figureType);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_Black_DownLeftCorner_ShouldReturn_4()
        {
            // arrange
            var postion = new Position.Position(0, 0);
            var team = TeamColor.Black;
            var figure = new King(postion, team);
            int expected = 4;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_Black_TopLeftCorner_ShouldReturn_Minus6()
        {
            // arrange
            var postion = new Position.Position(7, 0);
            var team = TeamColor.Black;
            var figure = new King(postion, team);
            int expected = -6;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_Black_AlmostTopRightCorner_ShouldReturn_Minus8()
        {
            // arrange
            var postion = new Position.Position(7, 6);
            var team = TeamColor.Black;
            var figure = new King(postion, team);
            int expected = -8;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_Black_AlmostDownRightCorner_ShouldReturn_6()
        {
            // arrange
            var postion = new Position.Position(0, 6);
            var team = TeamColor.Black;
            var figure = new King(postion, team);
            int expected = 6;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_Black_SomewhereInTheMiddle_ShouldReturn_Minus10()
        {
            // arrange
            var postion = new Position.Position(4, 3);
            var team = TeamColor.Black;
            var figure = new King(postion, team);
            int expected = -10;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        //
        [Test]
        public void GetPositionWeight_King_White_DownLeftCorner_ShouldReturn_Minus6()
        {
            // arrange
            var postion = new Position.Position(0, 0);
            var team = TeamColor.White;
            var figure = new King(postion, team);
            int expected = -6;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_White_TopLeftCorner_ShouldReturn_4()
        {
            // arrange
            var postion = new Position.Position(7, 0);
            var team = TeamColor.White;
            var figure = new King(postion, team);
            int expected = 4;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_White_AlmostTopRightCorner_ShouldReturn_6()
        {
            // arrange
            var postion = new Position.Position(7, 6);
            var team = TeamColor.White;
            var figure = new King(postion, team);
            int expected = 6;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_White_AlmostDownRightCorner_ShouldReturn_Minus8()
        {
            // arrange
            var postion = new Position.Position(0, 6);
            var team = TeamColor.White;
            var figure = new King(postion, team);
            int expected = -8;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
        
        [Test]
        public void GetPositionWeight_King_White_SomewhereInTheMiddle_ShouldReturn_Minus8()
        {
            // arrange
            var postion = new Position.Position(4, 3);
            var team = TeamColor.White;
            var figure = new King(postion, team);
            int expected = -8;
            var weights = new OrdinaryChessBoardFigurePositionWeight();

            // act
            var result = weights.GetPositionWeight(figure);
            
            // assert
            Assert.AreEqual(expected,result);
        }
    }
}