using System;
using System.Collections.Generic;

namespace Chess.GameSaver
{
    public class SaveRepository
    {
        public HashSet<string> Files { get; }

        private readonly string _directory;
        private readonly string _repositorySaveFilePath;
        public SaveRepository(string directory, string repositorySaveFilePath)
        {
            _directory = directory;
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            _repositorySaveFilePath = repositorySaveFilePath;
            Files = ChessGameSerializer.TryReadFromFile<HashSet<string>>(directory + repositorySaveFilePath);
        }

        public bool Contains(string filePath)
        {
            return Files.Contains(filePath);
        }

        public void Save(string filePath, ChessGameState state)
        {
            ChessGameSerializer.SaveInFile(_directory + filePath, state);
            Files.Add(filePath);
            ChessGameSerializer.SaveInFile(_directory + _repositorySaveFilePath, Files);
        }

        public ChessGameState Read(string filePath)
        {
            if (Files.Contains(filePath))
            {
                return ChessGameSerializer.ReadFromFile<ChessGameState>(_directory + filePath);
            }

            throw new ArgumentException($"File {filePath} not exist");
        }

        public bool Delete(string filePath)
        {
            if (Files.Contains(filePath) || ChessGameSerializer.ClearFile(_directory + filePath))
            {
                Files.Remove(filePath);
                ChessGameSerializer.SaveInFile(_directory + _repositorySaveFilePath, Files);
                return true;
            }
            return false;
        }
    }
}