using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UIDisplay.ApplicationStartMessage();

            PlayerInfoModel activePlayer = UILogic.CreatePlayer("Player 1");
            PlayerInfoModel opponent = UILogic.CreatePlayer("Player 2");
            
            PlayerInfoModel winner = null;
            do
            {
                UILogic.TakeTurn(activePlayer, opponent);

                //Determine if the game is over
                if (GameLogic.GameIsOver(opponent) == true)
                {
                    UIDisplay.EndGameMessage(activePlayer);

                    winner = activePlayer;
                }

                else
                {
                    activePlayer.TotalTurns++;

                    //Switch player spots for next turn
                    (activePlayer, opponent) = (opponent, activePlayer);
                }

            } while (winner == null);

            Console.ReadLine();
        }
    }
}