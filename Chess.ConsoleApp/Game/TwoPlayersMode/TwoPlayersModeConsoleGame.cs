using System;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.Game.GameManager;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Position;

namespace Chess.ConsoleApp.Game.TwoPlayersMode
{
    public class TwoPlayersModeConsoleGame : IConsoleGame
    {
        private TeamColor _player1Color;
        private TeamColor _player2Color;
        private readonly GameConductor _gameConductor;
        private IMoveResult _moveResult;
        
        public TwoPlayersModeConsoleGame()
        {
            SetUserColors();
            _gameConductor = new GameConductor();
            _moveResult = _gameConductor.Start();
        }

        public TwoPlayersModeConsoleGame(ChessGameState state)
        {
            BoardDisplay.ShowBoard(state.LastGameMoveResult.GetBoard());
            SetUserColors();
            _gameConductor = new GameConductor(state);
            _moveResult = state.LastGameMoveResult;
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
                    _player1Color = TeamColor.White;
                    _player2Color = TeamColor.Black;
                    break;
                case 2:
                    _player1Color = TeamColor.Black;
                    _player2Color = TeamColor.White;
                    break;
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again.");
                    SetUserColors();
                    break;
            }

            Console.WriteLine($"User 1 has color: {_player1Color}\nUser 2 has color: {_player2Color}");
        }
        
        public void Start()
        {
            if (_player1Color == _gameConductor.CurrentMoveTeam())
            {
                User1Move(_moveResult);
            }
            else
            {
                User2Move(_moveResult);
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

        private IMoveResult MoveHelper()
        {
            var movePositions = GetMovePositions();
            IMoveResult moveResult = _gameConductor.DoMove(movePositions.From, movePositions.Destination);

            var isValid = moveResult.IsValidMove();
            while (isValid.Status != MoveResultStatus.Valid)
            {
                Console.WriteLine(isValid.Cause);
                Console.WriteLine("Try again");
                movePositions = GetMovePositions();
                moveResult = _gameConductor.DoMove(movePositions.From, movePositions.Destination);
                isValid = moveResult.IsValidMove();
            }

            return moveResult;
        }

        private IMoveResult SaveGame(IMoveResult moveResult, int currentPlayer)
        {
            Console.WriteLine("Under what name save the game?");
            string file = UserInteraction.ReadNotEmptyStringFromUser();
            var saveRepository = SaveRepository.GetDefaultRepository();

            var state = new ChessGameState()
            {
                IsEnded = moveResult.IsCheckMate(TeamColor.Black) || moveResult.IsCheckMate(TeamColor.White),
                PlayerMode = PlayerMode.TwoPlayers,
                CurrentMovingTeam = currentPlayer == 1 ? _player1Color : _player2Color,
                LastGameMoveResult = moveResult
            };
            
            saveRepository.Save(file+".bin", state);
            Console.WriteLine("Game saved.");
            return new StoppedMoveResult();
        }
        
        private IMoveResult NextTurn(IMoveResult moveResult, int playerNumber)
        {
            BoardDisplay.ShowBoard(moveResult.GetBoard());
            
            Console.WriteLine($"\n ==================== User {playerNumber} ===================== ");
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "1. Next move\n2. Save game", "Number should be positive. Please try again");
            switch (choice)
            {
                case 1:
                    return MoveHelper();
                case 2:
                    return SaveGame(moveResult, playerNumber);
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again");
                    return NextTurn(moveResult, playerNumber);
            }
        }
        
        private void User1Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_player1Color))
            {
                EndGame(_player2Color);
            }

            moveResult = NextTurn(moveResult, 1);
            if (moveResult.IsValidMove().Status == MoveResultStatus.Stopped)
            {
                return;
            }

            User2Move(moveResult);
        }

        private void User2Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_player2Color))
            {
                EndGame(_player1Color);
            }
            
            moveResult = NextTurn(moveResult,2);
            if (moveResult.IsValidMove().Status == MoveResultStatus.Stopped)
            {
                return;
            }
            
            User1Move(moveResult);
        }

        private void EndGame(TeamColor winner)
        {
            Console.WriteLine("\n \n \n \n");
            Console.WriteLine($"Team {winner} wins!");
        }
    }
}