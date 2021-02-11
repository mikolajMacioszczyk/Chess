using System;
using Chess.ConsoleApp.Helpers;
using Chess.Enums;
using Chess.Game.GameConductor;
using Chess.Game.MoveResult;
using Chess.Models.Player;
using Chess.Models.Position;
using PlayerComputerAI.AI;

namespace Chess.ConsoleApp.Game.SinglePlayerMode
{
    public class SinglePlayerModeConsoleGame : IConsoleGame
    {
        private IGameConductor _gameConductor;
        private ComputerPlayer _computer;
        private Player _player;

        public SinglePlayerModeConsoleGame()
        {
            _player = UserInteraction.GetPlayerFromUser();
            TeamColor computerColor = _player.TeamColor == TeamColor.Black ? TeamColor.White : TeamColor.Black;
            int diff = (int) UserInteraction.GetDifficultyLevelFromUser();
            _computer = new ComputerPlayer(computerColor, diff + 1, diff);
            _gameConductor = new GameConductor();
        }
        
        public void Start()
        {
            var moveResult = _gameConductor.Start();
            if (_player.TeamColor == TeamColor.White)
            {
                PlayerMove(moveResult);
            }
            else
            {
                ComputerMove(moveResult);
            }
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
            
            Console.WriteLine($"\n ==================== {_player} ===================== ");
            Position from = UserInteraction.GetPositionFromUser("Position of your figure: ");
            Position destination = UserInteraction.GetPositionFromUser("Destination position: ");
            //
            IMoveResult newMoveResult = _gameConductor.DoMove(from, destination);
            var isValid = newMoveResult.IsValidMove();
            while (isValid.Status != MoveResultStatus.Valid)
            {
                Console.WriteLine($"Invalid move: {isValid.Cause}");
                PlayerMove(moveResult);
            }
            BoardDisplay.ShowFullInfoBoard(newMoveResult);
            
            ComputerMove(newMoveResult);
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
    }
}