using System;
using System.IO;
using Chess.Enums;
using Chess.GameSaver;
using NUnit.Framework;

namespace Chess.UnitTests.GameSaverUnitTests
{
    [TestFixture]
    public class SaveRepositoryUnitTests
    {
        [Test]
        public void Save_EmptyRepository_ShouldSave()
        {
            // arrange
            const string repoDirectory = "Test1/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Save_EmptyRepository_ShouldSave_Repo.bin";
            const string entryPath = "Test_Save_EmptyRepository_ShouldSave_Entry.bin";
            var gameState = new ChessGameState()
            {
                IsEnded = true,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var repo = new SaveRepository(repoDirectory, repoPath);
            
            // act
            bool isInRepoBefore = repo.Contains(entryPath);
            repo.Save(entryPath, gameState);
            bool isInRepoAfter = repo.Contains(entryPath);
            var fromRepo = repo.Read(entryPath);
            
            // assert
            Assert.False(isInRepoBefore);
            Assert.True(isInRepoAfter);
            Assert.NotNull(fromRepo);
            Assert.AreEqual(fromRepo.IsEnded, gameState.IsEnded);
            Assert.AreEqual(fromRepo.PlayerMode, gameState.PlayerMode);
            
            // clear
            Directory.Delete(repoDirectory, true);
        }

        [Test]
        public void Save_FileAlreadySaved_ShouldOverwrite()
        {
            // arrange
            const string repoDirectory = "Test2/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Save_FileAlreadySaved_ShouldOverwrite.bin";
            const string entryPath = "Test_Save_FileAlreadySaved_ShouldOverwrite.bin";
            var gameStateOld = new ChessGameState()
            {
                IsEnded = false,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var gameStateNew = new ChessGameState()
            {
                IsEnded = true,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var repo = new SaveRepository(repoDirectory, repoPath);
            repo.Save(entryPath, gameStateOld);
            
            // act
            bool isInRepoBefore = repo.Contains(entryPath);
            repo.Save(entryPath, gameStateNew);
            bool isInRepoAfter = repo.Contains(entryPath);
            var fromRepo = repo.Read(entryPath);
            
            // assert
            Assert.True(isInRepoBefore);
            Assert.True(isInRepoAfter);
            Assert.NotNull(fromRepo);
            Assert.AreEqual(fromRepo.IsEnded, gameStateNew.IsEnded);
            Assert.AreEqual(fromRepo.PlayerMode, gameStateNew.PlayerMode);
            
            // clear
            Directory.Delete(repoDirectory, true);
        }

        [Test]
        public void Contains_TrueAndFalse()
        {
            // arrange
            const string repoDirectory = "Test3/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Contains_Contains_ShouldReturnTrue.bin";
            const string entryPath = "Test_Contains_Contains_ShouldReturnTrue.bin";
            var gameState = new ChessGameState()
            {
                IsEnded = true,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var repo = new SaveRepository(repoDirectory, repoPath);
            repo.Save(entryPath, gameState);

            // act
            bool isInValid = repo.Contains(entryPath);
            bool isInInvalid = repo.Contains(entryPath+"!");
            
            // assert
            Assert.True(isInValid);
            Assert.False(isInInvalid);
            
            // clear
            Directory.Delete(repoDirectory, true);
        }
        
        [Test]
        public void Read_ValidPath_ShouldReturnSavedValue()
        {
            // arrange
            const string repoDirectory = "Test4/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Read_ValidPath_ShouldReturnSavedValue.bin";
            const string entryPath = "Test_Read_ValidPath_ShouldReturnSavedValue.bin";
            var gameState = new ChessGameState()
            {
                IsEnded = true,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var repoSaver = new SaveRepository(repoDirectory, repoPath);
            repoSaver.Save(entryPath, gameState);

            var repoReader = new SaveRepository(repoDirectory, repoPath);

            // act
            bool isInRepo = repoReader.Contains(entryPath);
            var fromRepo = repoReader.Read(entryPath);
            
            // assert
            Assert.True(isInRepo);
            Assert.AreEqual(fromRepo.IsEnded, gameState.IsEnded);
            Assert.AreEqual(fromRepo.PlayerMode, gameState.PlayerMode);

            // clear
            Directory.Delete(repoDirectory, true);
        }
        
        [Test]
        public void Read_InValidPath_ShouldThrowException()
        {
            // arrange
            const string repoDirectory = "Test5/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Read_InValidPath_ShouldThrowException.bin";
            const string entryPath = "Test_Read_InValidPath_ShouldThrowException.bin";
            
            var repoReader = new SaveRepository(repoDirectory, repoPath);
            
            // act
            bool isInRepo = repoReader.Contains(entryPath);
            Assert.Throws<ArgumentException>(() => repoReader.Read(entryPath));
            
            // assert
            Assert.False(isInRepo);
            
            // clear
            Directory.Delete(repoDirectory);
        }

        [Test]
        public void Delete_EntryInRepo_ShouldDelete_ShouldReturnTrue()
        {
            // arrange
            const string repoDirectory = "Test6/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Delete_EntryInRepo_ShouldDelete_ShouldReturnTrue.bin";
            const string entryPath = "Test_Delete_EntryInRepo_ShouldDelete_ShouldReturnTrue.bin";
            var gameState = new ChessGameState()
            {
                IsEnded = true,
                PlayerMode = PlayerMode.SinglePlayer
            };
            var repoSaver = new SaveRepository(repoDirectory, repoPath);
            repoSaver.Save(entryPath, gameState);

            var repoDeleter = new SaveRepository(repoDirectory, repoPath);

            // act
            bool isInRepo = repoDeleter.Contains(entryPath);
            bool result = repoDeleter.Delete(entryPath);
            
            // assert
            Assert.True(isInRepo);
            Assert.True(result);

            // clear
            Directory.Delete(repoDirectory, true);
        }
        
        [Test]
        public void Delete_EntryNotRepo_ShouldDelete_ShouldReturnFalse()
        {
            // arrange
            const string repoDirectory = "Test7/";
            Assert.False(Directory.Exists(repoDirectory));
            const string repoPath = "Delete_EntryInRepo_ShouldDelete_ShouldReturnTrue.bin";
            const string entryPath = "Test_Delete_EntryInRepo_ShouldDelete_ShouldReturnTrue.bin";

            var repoDeleter = new SaveRepository(repoDirectory, repoPath);

            // act
            bool isInRepo = repoDeleter.Contains(entryPath);
            bool result = repoDeleter.Delete(entryPath);
            
            // assert
            Assert.False(isInRepo);
            Assert.False(result);
            
            // clear
            Directory.Delete(repoDirectory, true);
        }
    }
}