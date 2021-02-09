using System.IO;
using Chess.Game.CheckVerfier;
using Chess.Game.MoveResult;
using Chess.Game.MoveValidator;
using Chess.Game.Team;
using Chess.GameSaver;
using Chess.Models.Board;
using Chess.Models.Figures.FigureImplementation;
using Chess.Models.Position;
using Chess.ViewModels.LastMoveViewModel;
using NUnit.Framework;

namespace Chess.UnitTests.GameSaverUnitTests
{
    [TestFixture]
    public class ChessGameSerializerUnitTests
    {
        [Test]
        public void Serialize_NextDeserialize_ValidObjects()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var validator = new OrdinaryBoardMoveValidator(board);
            var verifier = new OrdinaryBoardCheckVerifier(board, validator);
            var lastMoveVm = new LastMoveViewModel(new King(new Position(1,2), TeamColor.Black), 
                new Position(1,2), new Position(2, 3), null);
            var moveResult = new ValidMoveResult(board, verifier, validator, lastMoveVm);
            const bool isGameEnded = false;
            TeamColor currentTeam = TeamColor.Black;

            const string filePath = "Test_Serialize_NextDeserialize_ValidObjects.bin";

            ChessGameState gameState = new ChessGameState()
            {
                LastGameMoveResult = moveResult,
                IsEnded = isGameEnded,
                CurrentMovingTeam = currentTeam
            };

            // act
            ChessGameSerializer.SaveInFile(filePath, gameState);
            var fromFile = ChessGameSerializer.ReadFromFile(filePath);

            // assert
            Assert.AreEqual(fromFile.IsEnded, isGameEnded);
            Assert.AreEqual(fromFile.CurrentMovingTeam, currentTeam);
            Assert.AreEqual(fromFile.LastGameMoveResult.LastMoveFigureAndPositionFromAndDest().Item2,
                moveResult.LastMoveFigureAndPositionFromAndDest().Item2);
            
            // clear
            Assert.True(ChessGameSerializer.ClearFile(filePath));
        }
        
        [Test]
        public void Serialize_NextDeserialize_NullObject()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var validator = new OrdinaryBoardMoveValidator(board);
            var verifier = new OrdinaryBoardCheckVerifier(board, validator);
            var moveResult = new ValidMoveResult(board, verifier, validator, null);
            const bool isGameEnded = false;
            TeamColor currentTeam = TeamColor.Black;

            const string filePath = "Test_Serialize_NextDeserialize_NullObject.bin";

            ChessGameState gameState = new ChessGameState()
            {
                LastGameMoveResult = moveResult,
                IsEnded = isGameEnded,
                CurrentMovingTeam = currentTeam
            };

            // act
            ChessGameSerializer.SaveInFile(filePath, gameState);
            var fromFile = ChessGameSerializer.ReadFromFile(filePath);

            // assert
            Assert.AreEqual(fromFile.IsEnded, isGameEnded);
            Assert.AreEqual(fromFile.CurrentMovingTeam, currentTeam);
            
            // clear
            Assert.True(ChessGameSerializer.ClearFile(filePath));
        }
        
        [Test]
        public void Serialize_NextDeserialize_AlreadyWrittenFile()
        {
            // arrange
            var board = new OrdinaryChessBoard();
            var validator = new OrdinaryBoardMoveValidator(board);
            var verifier = new OrdinaryBoardCheckVerifier(board, validator);
            LastMoveViewModel lastMoveVm = new LastMoveViewModel(new King(new Position(1,2), TeamColor.Black), 
                new Position(1,2), new Position(2, 3), null);
            var moveResult = new ValidMoveResult(board, verifier, validator, lastMoveVm);
            const bool isGameEnded = false;
            TeamColor currentTeam = TeamColor.Black;

            const string filePath = "Serialize_NextDeserialize_AlreadyWrittenFile.bin";

            ChessGameState gameState = new ChessGameState()
            {
                LastGameMoveResult = moveResult,
                IsEnded = isGameEnded,
                CurrentMovingTeam = currentTeam
            };

            // act
            ChessGameSerializer.SaveInFile(filePath, gameState);
            ChessGameSerializer.SaveInFile(filePath, gameState);
            var fromFile = ChessGameSerializer.ReadFromFile(filePath);

            // assert
            Assert.AreEqual(fromFile.IsEnded, isGameEnded);
            Assert.AreEqual(fromFile.CurrentMovingTeam, currentTeam);
            Assert.AreEqual(fromFile.LastGameMoveResult.LastMoveFigureAndPositionFromAndDest().Item2,
                moveResult.LastMoveFigureAndPositionFromAndDest().Item2);
            
            // clear
            Assert.True(ChessGameSerializer.ClearFile(filePath));
        }

        [Test]
        public void ClearNotExitingFile_ShouldReturnFalse()
        {
            // arrange
            const string filePath = "Test_ClearNotExitingFile_ShouldReturnFalse.bin";
            
            // act
            
            // assert
            Assert.False(ChessGameSerializer.ClearFile(filePath));
        }
    }
}