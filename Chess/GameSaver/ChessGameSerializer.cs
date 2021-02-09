using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chess.GameSaver
{
    public static class ChessGameSerializer
    {
        public static void SaveInFile(string path, ChessGameState obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static ChessGameState ReadFromFile(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            ChessGameState gameState = (ChessGameState)formatter.Deserialize(stream);
            stream.Close();
            return gameState;
        }

        public static bool ClearFile(string path, int repeats = 10)
        {
            if (repeats < 0 || !File.Exists(path))
            {
                return false;
            }
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception ignored)
            {
                return ClearFile(path, repeats - 1);
            }
        }
    }
}