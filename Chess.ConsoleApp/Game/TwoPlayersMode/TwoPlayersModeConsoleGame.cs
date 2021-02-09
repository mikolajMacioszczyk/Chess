using System;
using Chess.ConsoleApp.Helpers;
using Chess.Game.GameManager;
using Chess.Game.MoveResult;
using Chess.Game.Team;
using Chess.Models.Position;

namespace Chess.ConsoleApp.Game.TwoPlayersMode
{
    public class TwoPlayersModeConsoleGame : IConsoleGame
    {
        private TeamColor _user1Color;
        private TeamColor _user2Color;
        private readonly GameConductor _gameConductor;
        
        public TwoPlayersModeConsoleGame()
        {
            SetUserColors();
            _gameConductor = new GameConductor();
        }

        private void SetUserColors()
        {
            Console.WriteLine(" ======================== Team ========================");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" 1. White");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" 2. Black");
            Console.ForegroundColor = ConsoleColor.White;
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "Select color for User 1: ", "Expected positive number, please try again");
            switch (choice)
            {
                case 1:
                    _user1Color = TeamColor.White;
                    _user2Color = TeamColor.Black;
                    break;
                case 2:
                    _user1Color = TeamColor.Black;
                    _user2Color = TeamColor.White;
                    break;
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again.");
                    SetUserColors();
                    break;
            }

            Console.WriteLine($"User 1 has color: {_user1Color}\nUser 2 has color: {_user2Color}");
        }
        
        public void Start()
        {
            var moveResult = _gameConductor.Start();
            if (_user1Color == TeamColor.White)
            {
                User1Move(moveResult);
            }
            else
            {
                User2Move(moveResult);
            }
        }
        
        private struct MovePositions
        {
            public Position From { get; set; }
            public Position Destination { get; set; }
        }

        private MovePositions GetMovePositions()
        {
            Position from = UserInteraction.GetPositionFromUser("Position of your figure: ");
            Position destination = UserInteraction.GetPositionFromUser("Destination position: ");
            return new MovePositions() {From = from, Destination = destination};
        }

        private IMoveResult MoveHelper(IMoveResult moveResult, int userNumber)
        {
            BoardDisplay.ShowBoard(moveResult.GetBoard());

            Console.WriteLine($"\nUser {userNumber} move:\n");

            var movePositions = GetMovePositions();
            moveResult = _gameConductor.DoMove(movePositions.From, movePositions.Destination);

            var isValid = moveResult.IsValidMove();
            while (!isValid.IsValid)
            {
                Console.WriteLine(isValid.Cause);
                Console.WriteLine("Try again");
                Console.WriteLine($"\nUser {userNumber} move:");
                movePositions = GetMovePositions();
                moveResult = _gameConductor.DoMove(movePositions.From, movePositions.Destination);
                isValid = moveResult.IsValidMove();
            }

            return moveResult;
        }
        
        private void User1Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_user1Color))
            {
                EndGame(_user2Color);
            }

            moveResult = MoveHelper(moveResult, 1);

            User2Move(moveResult);
        }

        private void User2Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_user2Color))
            {
                EndGame(_user1Color);
            }
            
            moveResult = MoveHelper(moveResult,2);

            User1Move(moveResult);
        }

        private void EndGame(TeamColor winner)
        {
            Console.WriteLine("\n \n \n \n");
            Console.WriteLine($"Team {winner} wins!");
        }
    }
}