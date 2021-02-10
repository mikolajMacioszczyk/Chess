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
        
        private void ShowBoard(IMoveResult moveResult)
        {
            BoardDisplay.DisplaySmashed(moveResult.AllSmashedFigures(), TeamColor.White);
            Console.WriteLine();
            BoardDisplay.DisplaySmashed(moveResult.AllSmashedFigures(), TeamColor.Black);
            BoardDisplay.ShowBoard(moveResult.GetBoard());
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

        enum PlayerAction
        {
            Submit,
            Undo,
            Save
        }
        
        private PlayerAction PlayerActionApprove(ref IMoveResult moveResult, int currentPlayer)
        {
            int choice = UserInteraction.GetPositiveNumberFromUser(
                "1. Submit\n2. Undo\n3. Save", "Expected positive number. Try again");
            if (choice == 1)
                return PlayerAction.Submit;

            if (choice == 2)
            {
                Console.WriteLine(_gameConductor.Undo() ? "The move has been withdrawn" : "You cannot withdraw this move");
                return PlayerAction.Undo;
            }

            if (choice == 3)
            {
                moveResult = SaveGame(moveResult, currentPlayer == 0 ? 1 : 0);
                return PlayerAction.Save;
            }
            
            Console.WriteLine($"Option {choice} not found. Please try again.");
            return PlayerActionApprove(ref moveResult, currentPlayer);
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
            ShowBoard(moveResult);
            return moveResult;
        }
        private IMoveResult SaveGame(IMoveResult moveResult, int currentPlayer)
        {
            Console.WriteLine("Under what name save the game?");
            string file = UserInteraction.ReadNotEmptyStringFromUser();
            var saveRepository = SaveRepository.GetDefaultRepository();

            bool isEnded = moveResult.IsCheckMate(TeamColor.Black) || moveResult.IsCheckMate(TeamColor.White);
            var state = new ChessGameState(moveResult, isEnded, _players, 
                _players[currentPlayer].TeamColor, PlayerMode.TwoPlayers);
            
            saveRepository.Save(file+".bin", state);
            Console.WriteLine("Game saved.");
            return new StoppedMoveResult();
        }

        private void VerifyCheckAndDisplayMessage(IMoveResult moveResult, int playerNumber)
        {
            if (moveResult.IsCheck(_players[playerNumber].TeamColor))
            {
                Console.WriteLine("!!!");
                Console.WriteLine("Beware, your king is in check!");
                Console.WriteLine("!!!");
            }
        }
        
        private IMoveResult NextTurn(IMoveResult moveResult, int playerNumber)
        {
            ShowBoard(moveResult);
            VerifyCheckAndDisplayMessage(moveResult, playerNumber);
            
            Console.WriteLine($"\n ==================== {_players[playerNumber]} ===================== ");
            var newMoveResult = MoveHelper();
            
            if (newMoveResult.IsLastMoveSmash())
            {
                var smashed = newMoveResult.SmashedFigure();
                Console.WriteLine($"Player {_players[playerNumber].Name} smashed " +
                                  $"{_players[playerNumber == 0 ? 1 : 0].Name}'s {smashed.FigureType}");
            }
            
            var playerApprove = PlayerActionApprove(ref newMoveResult, playerNumber);
            if (playerApprove == PlayerAction.Undo)
            {
                return NextTurn(moveResult, playerNumber);
            }

            return newMoveResult;
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