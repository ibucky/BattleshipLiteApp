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
            
            bool gameIsOver = false;
            do
            {

                //Determine if the game is over
                gameIsOver = GameLogic.GameIsOver(activePlayer, opponent);

            } while (gameIsOver = false);

            Console.ReadLine();
        }
    }
}