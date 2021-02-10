using System;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.Game.GameManager;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Player;
using Chess.Models.Position;

namespace Chess.ConsoleApp.Game.TwoPlayersMode
{
    public class TwoPlayersModeConsoleGame : IConsoleGame
    {
        private readonly Player[] _players = new Player[2];
        private readonly GameConductor _gameConductor;
        private readonly IMoveResult _moveResult;
        
        public TwoPlayersModeConsoleGame()
        {
            SetUsers();
            _gameConductor = new GameConductor();
            _moveResult = _gameConductor.Start();
        }

        public TwoPlayersModeConsoleGame(ChessGameState state)
        {
            BoardDisplay.ShowBoard(state.LastGameMoveResult.GetBoard());
            _players = state.Players;
            _gameConductor = new GameConductor(state);
            _moveResult = state.LastGameMoveResult;
        }
        
        private void SetUsers()
        {
            var colors = UserInteraction.GetColorFromPlayer();
            _players[0] = colors.Item1;
            _players[1] = colors.Item2;
        }
        
        public void Start()
        {
            if (_players[0].TeamColor == _gameConductor.CurrentMoveTeam())
                User1Move(_moveResult);
            else
                User2Move(_moveResult);
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

        private bool UserSubmitMovement()
        {
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "1. Submit\n2. Undo", "Expected positive number. Try again");
            if (choice == 1)
                return true;

            if (choice == 2)
            {
                Console.WriteLine(_gameConductor.Undo() ? "The move has been withdrawn" : "You cannot withdraw this move");
                return false;
            }
            
            Console.WriteLine($"Option {choice} not found. Please try again.");
            return UserSubmitMovement();
        }

        private IMoveResult MoveHelper()
        {
            var movePositions = GetMovePositions();
            IMoveResult moveResult = _gameConductor.DoMove(movePositions.From, movePositions.Destination);
            var isValid = moveResult.IsValidMove();
            while (isValid.Status != MoveResultStatus.Valid)
            {
                Console.WriteLine($"Invalid move: {isValid.Cause}");
                return MoveHelper();
            }
            BoardDisplay.ShowBoard(moveResult.GetBoard());
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
                Players = _players,
                CurrentMovingTeam = _players[currentPlayer-1].TeamColor,
                LastGameMoveResult = moveResult
            };
            
            saveRepository.Save(file+".bin", state);
            Console.WriteLine("Game saved.");
            return new StoppedMoveResult();
        }
        
        private IMoveResult NextTurn(IMoveResult moveResult, int playerNumber)
        {
            BoardDisplay.ShowBoard(moveResult.GetBoard());
            
            Console.WriteLine($"\n ==================== {_players[playerNumber]} ===================== ");
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "1. Next move\n2. Save game", "Number should be positive. Please try again");
            switch (choice)
            {
                case 1:
                    var newMoveResult = MoveHelper();
                    if (!UserSubmitMovement())
                    {
                        return NextTurn(moveResult, playerNumber);
                    }
                    return newMoveResult;
                case 2:
                    return SaveGame(moveResult, playerNumber);
                default:
                    Console.WriteLine($"Option {choice} not found. Please try again");
                    return NextTurn(moveResult, playerNumber);
            }
        }
        
        private void User1Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_players[0].TeamColor))
            {
                EndGame(1);
            }

            moveResult = NextTurn(moveResult, 0);
            if (moveResult.IsValidMove().Status == MoveResultStatus.Stopped)
            {
                return;
            }

            User2Move(moveResult);
        }

        private void User2Move(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_players[1].TeamColor))
            {
                EndGame(1);
            }
            
            moveResult = NextTurn(moveResult,1);
            if (moveResult.IsValidMove().Status == MoveResultStatus.Stopped)
            {
                return;
            }
            
            User1Move(moveResult);
        }

        private void EndGame(int winner)
        {
            Console.WriteLine("\n \n \n \n");
            Console.WriteLine($"Player {_players[winner]} wins!");
        }
    }
}