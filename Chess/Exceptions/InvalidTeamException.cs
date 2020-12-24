using System;
using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.Exceptions
{
    public class InvalidTeamException : Exception
    {
        private TeamColor _teamColor;
        private Position _position;

        public InvalidTeamException(TeamColor teamColor, Position position)
        {
            _teamColor = teamColor;
            _position = position;
        }

        public override string Message => $"Try to move figure from position {_position}, which do not contains team {_teamColor} figure";
    }
}