using System;
using Chess.Enums;

namespace Chess.Models.Player
{
    [Serializable]
    public class Player
    {
        public string Name { get; }
        public TeamColor TeamColor { get; }

        public Player(string name, TeamColor teamColor)
        {
            Name = name;
            TeamColor = teamColor;
        }

        public override string ToString()
        {
            return $"{Name} - {TeamColor}";
        }
    }
}