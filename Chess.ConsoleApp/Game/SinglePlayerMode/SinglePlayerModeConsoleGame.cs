using System;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.Game.GameConductor;
using Chess.Game.MoveResult;
using Chess.GameSaver;
using Chess.Models.Player;
using Chess.Models.Position;
using PlayerComputerAI.AI;

namespace Chess.ConsoleApp.Game.SinglePlayerMode
{
    public class SinglePlayerModeConsoleGame : IConsoleGame
    {
        private readonly IGameConductor _gameConductor;
        private readonly ComputerPlayer _computer;
        private readonly Player _player;
        private readonly int _difficulty;
        private readonly IMoveResult _startMoveResult;

        public SinglePlayerModeConsoleGame()
        {
            _player = UserInteraction.GetPlayerFromUser();
            _difficulty = (int) UserInteraction.GetDifficultyLevelFromUser();
            _gameConductor = new GameConductor();
            _startMoveResult = _gameConductor.Start();
            TeamColor computerColor = _player.TeamColor == TeamColor.Black ? TeamColor.White : TeamColor.Black;
            _computer = new ComputerPlayer(computerColor, _difficulty + 1, _difficulty);
        }
        
        public SinglePlayerModeConsoleGame(ChessGameState state)
        {
            _player = state.Players[0];
            _difficulty = state.DifficultyLevel;
            _gameConductor = new GameConductor(state);
            _startMoveResult = state.LastGameMoveResult;
            TeamColor computerColor = _player.TeamColor == TeamColor.Black ? TeamColor.White : TeamColor.Black;
            _computer = new ComputerPlayer(computerColor, _difficulty + 1, _difficulty);
        }
        public void Start()
        {
            if (_player.TeamColor == _gameConductor.CurrentMoveTeam())
                PlayerMove(_startMoveResult);
            else
                ComputerMove(_startMoveResult);
        }
        
        private void PlayerMove(IMoveResult moveResult)
        {
            BoardDisplay.ShowFullInfoBoard(moveResult);
            if (moveResult.IsCheckMate(_player.TeamColor))
            {
                Console.WriteLine("\n \n \n \n");
                Console.WriteLine($"Computer wins!");
                return;
            }

            moveResult = PlayerMoveHelper();
            if (moveResult.IsValidMove().Status != MoveResultStatus.Stopped)
            {
                ComputerMove(moveResult);
            }
        }

        private void ComputerMove(IMoveResult moveResult)
        {
            if (moveResult.IsCheckMate(_computer.MyTeamColor))
            {
                Console.WriteLine("\n \n \n \n");
                Console.WriteLine($"Player {_player.Name} wins!");
                return;
            }
            
            Console.WriteLine($"==================== Computer - {_computer.MyTeamColor} =====================");
            var nextMove = _computer.NextMove(moveResult.GetBoard());
            moveResult = _gameConductor.DoMove(nextMove.From, nextMove.Destination);

            Console.WriteLine($"Computer has moved the figure from the {nextMove.From} position to the {nextMove.Destination} position");
            PlayerMove(moveResult);
        }

        private IMoveResult PlayerMoveHelper()
        {
            Console.WriteLine($"\n ==================== {_player} ===================== ");
            Position from = UserInteraction.GetPositionFromUser("Position of your figure: ");
            Position destination = UserInteraction.GetPositionFromUser("Destination position: ");
            
            IMoveResult moveResult = _gameConductor.DoMove(from, destination);
            var isValid = moveResult.IsValidMove();
            while (isValid.Status != MoveResultStatus.Valid)
            {
                Console.WriteLine($"Invalid move: {isValid.Cause}");
                PlayerMoveHelper();
            }
            BoardDisplay.ShowFullInfoBoard(moveResult);

            Console.WriteLine("Select action");
            Console.WriteLine("1. Submit");
            Console.WriteLine("2. Undo");
            Console.WriteLine("3. Save");

            int choice = UserInteraction.GetNumberFromUser("", "Option not found, please try again", 1,3);
            if (choice == 2)
            {
                Console.WriteLine(_gameConductor.Undo() ? "The move has been reversed." : "Cannot reverse move.");
                BoardDisplay.ShowFullInfoBoard(moveResult);
                return PlayerMoveHelper();
            }
            if (choice == 3)
            {
                return SaveGame(moveResult);
            }
            return moveResult;
        }

        private IMoveResult SaveGame(IMoveResult moveResult)
        {
            Console.WriteLine("Under what name save the game?");
            string file = UserInteraction.ReadNotEmptyStringFromUser();
            var saveRepository = SaveRepository.GetDefaultRepository();

            bool isEnded = moveResult.IsCheckMate(TeamColor.Black) || moveResult.IsCheckMate(TeamColor.White);
            var state = new ChessGameState(moveResult, isEnded, new []{_player}, 
                _computer.MyTeamColor, PlayerMode.SinglePlayer, _difficulty);
            
            saveRepository.Save(file+".bin", state);
            Console.WriteLine("Game saved.");
            return new StoppedMoveResult();
        }
    }
}