using System;
using System.Collections.Generic;

namespace Chess.GameSaver
{
    public class SaveRepository
    {
        public HashSet<string> Files { get; }
        private static SaveRepository _instance;

        public static SaveRepository GetRepository()
        {
            if (_instance == null)
            {
                _instance = new SaveRepository(Directory + RepositorySaveFilePath);
            }
            return _instance;
        }
        private static readonly string Directory = "Saved/";
        private static readonly string RepositorySaveFilePath = "SaveRepository.bin";
        private SaveRepository(string path)
        {
            Files = ChessGameSerializer.TryReadFromFile<HashSet<string>>(path);
        }

        public bool Contains(string filePath)
        {
            return Files.Contains(filePath);
        }

        public void Save(string filePath, ChessGameState state)
        {
            ChessGameSerializer.SaveInFile(Directory + filePath, state);
            Files.Add(filePath);
            ChessGameSerializer.SaveInFile(Directory + RepositorySaveFilePath, Files);
        }

        public ChessGameState Read(string filePath)
        {
            if (Files.Contains(filePath))
            {
                return ChessGameSerializer.ReadFromFile<ChessGameState>(Directory + filePath);
            }

            throw new ArgumentException($"File {filePath} not exist");
        }

        public bool Delete(string filePath)
        {
            if (Files.Contains(filePath) || ChessGameSerializer.ClearFile(Directory + filePath))
            {
                Files.Remove(filePath);
                ChessGameSerializer.SaveInFile(Directory + RepositorySaveFilePath, Files);
                return true;
            }
            return false;
        }
    }
}