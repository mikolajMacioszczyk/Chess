using System;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.Game.GameConductor;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Player;
using Chess.Models.Position;
using Chess.ViewModels;

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
            var colors = UserInteraction.GetColorFromTwoPlayers();
            _players[0] = colors.Item1;
            _players[1] = colors.Item2;
        }
        
        public void Start()
        {
            UserMove(_moveResult, _players[0].TeamColor == _gameConductor.CurrentMoveTeam() ? 0 : 1);
        }
        
        private void UserMove(IMoveResult moveResult, int userIdx)
        {
            while (!moveResult.IsCheckMate(_players[userIdx].TeamColor))
            {
                moveResult = NextTurn(moveResult, userIdx);
                if (moveResult.IsValidMove().Status == MoveResultStatus.Stopped)
                {
                    return;
                }

                userIdx = userIdx == 1 ? 0 : 1;
            }
            EndGame(userIdx);
        }

        private MovePositions GetMovePositions()
        {
            Position from = UserInteraction.GetPositionFromUser("Position of your figure: ");
            Position destination = UserInteraction.GetPositionFromUser("Destination position: ");
            return new MovePositions() {From = from, Destination = destination};
        }

        private PlayerAction PlayerActionApprove(ref IMoveResult moveResult, int currentPlayer)
        {
            int choice = UserInteraction.GetNumberFromUser(
                "1. Submit\n2. Undo\n3. Save", $"Option not found. Please try again.", 1, 3);
            if (choice == 1)
                return PlayerAction.Submit;

            if (choice == 2)
            {
                Console.WriteLine(_gameConductor.Undo() ? "The move has been withdrawn" : "You cannot withdraw this move");
                return PlayerAction.Undo;
            }
            moveResult = SaveGame(moveResult, currentPlayer == 0 ? 1 : 0);
            return PlayerAction.Save;
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
            BoardDisplay.ShowFullInfoBoard(moveResult);
            return moveResult;
        }
        private IMoveResult SaveGame(IMoveResult moveResult, int currentPlayer)
        {
            Console.WriteLine("Under what name save the game?");
            string file = UserInteraction.ReadNotEmptyStringFromUser();
            var saveRepository = SaveRepository.GetDefaultRepository();

            bool isEnded = moveResult.IsCheckMate(TeamColor.Black) || moveResult.IsCheckMate(TeamColor.White);
            var state = new ChessGameState(moveResult, isEnded, _players, 
                _players[currentPlayer].TeamColor, PlayerMode.TwoPlayers, 0);
            
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
            BoardDisplay.ShowFullInfoBoard(moveResult);
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

        private void EndGame(int winner)
        {
            Console.WriteLine("\n \n \n \n");
            Console.WriteLine($"Player {_players[winner]} wins!");
        }
    }
}