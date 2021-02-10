using System;
using Chess.Enums;

namespace Chess.Models.Player
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public TeamColor TeamColor { get; set; }

        public override string ToString()
        {
            return $"{Name} - {TeamColor}";
        }
    }
}